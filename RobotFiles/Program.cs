using System;
using MonoBrick.EV3;
//using MonoBrick.NXT;

public class Program{
    static void Main(string[] args){
        var brain = new Brick<Sensor, Sensor, Sensor, Sensor>("com4"); //set up brick here to allow all functions access
        try {

            brain.Connection.Open(); //connect to brick

            brain.Sensor1 = new UltrasonicSensor(UltrasonicMode.Centimeter); // set ultrasonic sensor mode
            brain.Sensor2 = new ColorSensor(ColorMode.Color); // set color sensor mode
            brain.Sensor3 = new TouchSensor(TouchMode.Boolean); // set touch sensor to button mode

            brain.Vehicle.LeftPort = MotorPort.OutA;    //
            brain.Vehicle.RightPort = MotorPort.OutD;   // initialise 'vehicle' motors
            brain.Vehicle.ReverseLeft = false;          //
            brain.Vehicle.ReverseRight = false;          //

            sbyte speed = 50;                           // robot's speed, will need changing
            sbyte turn_percent = 25;
            string surfaceColor = "White";                  //colour for robot to stay on
            string maxDistance = "6 cm";                      // distance where 'collision' becomes possible

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
                if (brain.Sensor2.ReadAsString() != surfaceColor | brain.Sensor1.ReadAsString() == maxDistance)
                {
                    Console.WriteLine("saw something");
                    brain.Vehicle.Brake();
                    brain.Vehicle.Backward(speed);
                    brain.Vehicle.SpinLeft(speed);
                    /*if (randomVal.Next(0, 1) == 1) //pick a random direction
                    {
                        brain.Vehicle.SpinLeft(speed); //rotate left 
                        Console.WriteLine("spun left");
                    }
                    else
                    {
                        brain.Vehicle.SpinRight(speed); //rotate right
                        Console.WriteLine("spun right");
                    } */
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