using System;
using MonoBrick.EV3;
//using MonoBrick.NXT;

public class Program{
    static void Main(string[] args){
        //var brain = new Brick<Sensor, Sensor, Sensor, Sensor>("usb"); //set up brick here to allow all functions access
        try {
            /*
            brain.Connection.Open(); //connect to brick

            brain.Sensor1 = new UltrasonicSensor(UltrasonicMode.Centimeter); // set ultrasonic sensor mode
            brain.Sensor2 = new ColorSensor(ColorMode.Color); // set color sensor mode
            brain.Sensor3 = new TouchSensor(TouchMode.Boolean); // set touch sensor to button mode

            brain.Vehicle.LeftPort = MotorPort.OutA;    //
            brain.Vehicle.RightPort = MotorPort.OutD;   // initialise 'vehicle' motors
            brain.Vehicle.ReverseLeft = false;          //
            brain.Vehicle.ReverseRight = true;          //

            sbyte speed = 50;                           // robot's speed, will need changing
            string surfaceColor = "6";                  //colour for robot to stay on
            string maxDistance = "6 cm";                      // distance where 'collision' becomes possible
                         //
            Console.WriteLine("Press Q to exit program");   // set up exit clause
            ConsoleKeyInfo quitKey;
            quitKey = Console.ReadKey(true);

            Random randomVal = new Random();    //random number for when i need it
             */
            while (true)
            {
                /*
                Console.WriteLine(brain.Sensor1.ReadAsString());
                quitKey = Console.ReadKey(true); //check if Q has been pressed
                bool edgeMode = false; //check if edge-sensing mode enabled
                /*
                if (brain.Sensor1.ReadAsString() == "1")
                { //enable/disable edge-sensing mode depending on previous state
                    if (edgeMode == false)
                    {
                        edgeMode = true;
                    }
                    else
                    {
                        edgeMode = false;
                    }
                }
                
                if (edgeMode || brain.Sensor1.ReadAsString() == maxDistance ) // collision checker
                {
                    if (brain.Sensor3.ReadAsString() != surfaceColor) //if robot leaves surface, could be dodgy if it goes at an angle
                    {
                        brain.Vehicle.Backward(speed); //reverse
                        if (randomVal.Next(0, 1) == 1) //pick a random direction
                        {
                            brain.Vehicle.SpinLeft(speed); //rotate left 
                        }
                        else
                        {
                            brain.Vehicle.SpinRight(speed); //rotate right
                        }
                    }
                }
                brain.Vehicle.Forward(speed);
                //brain.Vehicle.Off();
                Console.WriteLine(brain.Sensor1.ReadAsString());
                */
                Console.WriteLine("hey");
            }  
            }
        catch(Exception e) {
            Console.WriteLine("Error: "+ e.Message);
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
        finally
        {
            //brain.Connection.Close(); //close connection
        }
    }      
}