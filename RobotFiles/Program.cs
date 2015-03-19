using System;
using MonoBrick.EV3;
//using MonoBrick.NXT;

public class Program{
    static void Main(string[] args){
        var brain = new Brick<Sensor, Sensor, Sensor, Sensor>("com6"); //set up brick here to allow all functions access
        try {

            brain.Connection.Open(); //connect to brick

            brain.Sensor1 = new UltrasonicSensor(UltrasonicMode.Centimeter); // set ultrasonic sensor mode
            brain.Sensor2 = new ColorSensor(ColorMode.Color); // set color sensor mode
            brain.Sensor3 = new TouchSensor(TouchMode.Boolean); // set touch sensor to button mode

            brain.Vehicle.LeftPort = MotorPort.OutA;    //
            brain.Vehicle.RightPort = MotorPort.OutD;   // initialise 'vehicle' motors
            brain.Vehicle.ReverseLeft = false;          //
            brain.Vehicle.ReverseRight = true;          //

            sbyte speed = 20;                           // robot's speed, will need changing
            string surfaceColor = "6";                  //colour for robot to stay on
            string maxDistance = "6";                      // distance where 'collision' becomes possible

            ConsoleKeyInfo quitKey;                         //
            quitKey = Console.ReadKey(true);
            Console.WriteLine("Press Q to exit program");   // set up exit clause

            Random randomVal = new Random();    //random number for when i need it
            while (true)
            {
                quitKey = Console.ReadKey(true); //check if Q has been pressed
                bool edgeMode = false; //check if edge-sensing mode enabled
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
                //if robot leaves surface, could be dodgy if it goes at an angle
                // I sincerely apologise for this god-awful conditional here
                if ((brain.Sensor3.ReadAsString() != surfaceColor && edgeMode) || brain.Sensor1.ReadAsString() == maxDistance)
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
                brain.Vehicle.Forward(speed);
                Console.WriteLine("got here");
                if (quitKey.Key == ConsoleKey.Q)
                {
                    break;
                }
            }
        }
        catch(Exception e) {
            Console.WriteLine("Error: "+ e.Message);
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
        finally
        {
            brain.Connection.Close(); //close connection
        }
    }      
}