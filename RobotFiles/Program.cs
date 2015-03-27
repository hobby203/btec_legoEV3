using System;
using MonoBrick.EV3;
//using MonoBrick.NXT;

public class Program{
    static void Main(string[] args){
        var brain = new Brick<Sensor, Sensor, Sensor, Sensor>("com4"); //set up brick here to allow all functions access
        try {

            brain.Connection.Open(); //connect to brick

            brain.Sensor2 = new UltrasonicSensor(UltrasonicMode.Centimeter); // set ultrasonic sensor mode
            brain.Sensor3 = new ColorSensor(ColorMode.Color); // set color sensor mode
            //brain.Sensor3 = new TouchSensor(TouchMode.Boolean); // set touch sensor to button mode

            brain.Vehicle.LeftPort = MotorPort.OutA;    //
            brain.Vehicle.RightPort = MotorPort.OutD;   // initialise 'vehicle' motors
            brain.Vehicle.ReverseLeft = false;          //
            brain.Vehicle.ReverseRight = false;          //

            sbyte speed = 127;                          // robot's speed, will need changing
            sbyte turn_percent = 90;
            string surfaceColor = "Black";                  //colour for robot to stay on
            int maxDistance = 20;                      // distance where 'collision' becomes possible

            Console.WriteLine("Press any key to exit program");   // set up exit clause

            Random randomVal = new Random();    //random number for when i need it
            while (!Console.KeyAvailable)
            {
                bool edgeMode = false; //check if edge-sensing mode enabled
                Console.WriteLine(brain.Sensor3.ReadAsString());
                Console.WriteLine(brain.Sensor2.ReadAsString());
                //Console.WriteLine(brain.Sensor1.ReadAsString());
                if (brain.Sensor3.ReadAsString() == "1")
                { //enable/disable edge-sensing mode depending on previous state
                    Console.WriteLine("woooah the button was pressed");
                    if (edgeMode == false)
                    {
                        edgeMode = true;
                    }
                    else
                    {
                        edgeMode = false;
                    }
                }
                //if robot leaves surface, could be dodgy if it goes at an angle
                // I sincerely apologise for this god-awful conditional here
                string curDistance_string = brain.Sensor2.ReadAsString();
                curDistance_string = curDistance_string.Remove(2);
                int curDistance = int.Parse(curDistance_string);
                if (brain.Sensor3.ReadAsString() != surfaceColor | curDistance <= maxDistance)
                {
                    Console.WriteLine("saw something");
                    brain.Vehicle.Brake();
                    if (randomVal.Next(-1, 2) == 1) //pick a random direction
                    {
                        brain.Vehicle.TurnLeftReverse(speed, turn_percent);
                        Console.WriteLine("spun left");
                    }
                    else
                    {
                        brain.Vehicle.TurnRightReverse(speed, turn_percent);
                        Console.WriteLine("spun right");
                    } 
                }
                else
                {
                    brain.Vehicle.Forward(speed);
                }
                

                //Console.WriteLine("got to the end!");
            }
        }
        catch(Exception e) {
            Console.WriteLine("Error: "+ e.Message);
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
        finally
        {
            brain.Vehicle.Brake();
            brain.Connection.Close(); //close connection
        }
    }      
}