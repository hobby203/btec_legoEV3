using System;
using MonoBrick.EV3;
//using MonoBrick.NXT;

public class Program{
    static void Main(string[] args){
        //set up brick here to allow all functions access
        var brain = new Brick<Sensor, Sensor, Sensor, Sensor>("com6");
        try {
            Console.WriteLine("Hey hey");
            Console.ReadKey();
            //brain.Connection.Open(); //connect to brick

            //brain.Sensor2 = new UltrasonicSensor(UltrasonicMode.Centimeter);
            //brain.Sensor2.ReadAsString(); //idk if this'll work, test it

            brain.Sensor3 = new ColorSensor(ColorMode.Color);
            brain.Sensor1 = new TouchSensor(TouchMode.Boolean);

            brain.Vehicle.LeftPort = MotorPort.OutA;    //
            brain.Vehicle.RightPort = MotorPort.OutD;   // initialise 'vehicle' motors
            brain.Vehicle.ReverseLeft = false;          //
            brain.Vehicle.ReverseRight = true;          //
            sbyte speed = 20;                           // robot's speed, will need changing

            string surfaceColor = "6";                  //colour for robot to stay on

            ConsoleKeyInfo quitKey;                         //
            Console.WriteLine("Press Q to exit program");   // set up exit clause

            Random randomVal = new Random();    //random number for when i need it
            do
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

                if (edgeMode)
                {
                    if (brain.Sensor3.ReadAsString() != surfaceColor)
                        //if robot leaves surface, could be dodgy if it goes at an angle
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
                
            } while (quitKey.Key != ConsoleKey.Q);
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