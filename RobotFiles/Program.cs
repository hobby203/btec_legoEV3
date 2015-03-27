using System;
using MonoBrick.EV3;
//using MonoBrick.NXT;

public class Program{
    static void Main(string[] args){
        var brain = new Brick<Sensor, Sensor, Sensor, Sensor>("com4"); //set up brick information
        try {

            brain.Connection.Open(); //connect to brick

            brain.Sensor2 = new UltrasonicSensor(UltrasonicMode.Centimeter); // set ultrasonic sensor mode
            brain.Sensor3 = new ColorSensor(ColorMode.Color); // set color sensor mode

            brain.Vehicle.LeftPort = MotorPort.OutA;    //
            brain.Vehicle.RightPort = MotorPort.OutD;   // initialise 'vehicle' motors
            brain.Vehicle.ReverseLeft = false;
            brain.Vehicle.ReverseRight = false;

            sbyte speed = 127;                  // robot's speed, will need changing
            sbyte turn_percent = 90;            // how much the robot will turn if it hits something
            string surfaceColor = "Black";      // colour for robot to stay on
            int maxDistance = 20;               // distance where 'collision' becomes possible

            Console.WriteLine("Press any key to exit program");   // set up exit clause

            Random randomVal = new Random();    // random number for when i need it
            while (!Console.KeyAvailable)       // allows program to exit if you press a button
            {
                string curDistance_string = brain.Sensor2.ReadAsString();   // read current distance
                curDistance_string = curDistance_string.Remove(2);          // remove the 'cm' from the end
                int curDistance = int.Parse(curDistance_string);            // convert to an integer, for comparison
                if (brain.Sensor3.ReadAsString() != surfaceColor | curDistance <= maxDistance) // check if  object is getting too close
                {
                    brain.Vehicle.Brake();  // stop the motors, allows vehicle to reverse
                    if (randomVal.Next(-1, 2) == 1) // pick a random direction to turn
                    {
                        brain.Vehicle.TurnLeftReverse(speed, turn_percent); // move backwards whilst turning left
                    }
                    else
                    {
                        brain.Vehicle.TurnRightReverse(speed, turn_percent); // move backwards whilst turning right
                    } 
                }
                else
                {
                    brain.Vehicle.Forward(speed); // move forwards
                }
            }
        }
        catch(Exception e) { // catch errors from connecting to brick
            Console.WriteLine("Error: "+ e.Message);
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
        finally // once button has been pressed
        {
            brain.Vehicle.Brake(); // stop the vehicle motors
            brain.Connection.Close(); //close connection
        }
    }      
}