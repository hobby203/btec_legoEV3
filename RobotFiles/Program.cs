using System;
using MonoBrick.EV3;
//using MonoBrick.NXT;

public class Program{
    static void Main(string[] args){
        try {
            Console.WriteLine("Hey hey");
            Console.ReadKey();
            var brain = new Brick<Sensor, Sensor, Sensor, Sensor>("com6"); //set up brick here to allow all functions access
            brain.Connection.Open(); //connect to brick

            //brain.Sensor2 = new Sensor();
            //brain.Sensor2.ReadAsString(); //idk if this'll work, test it

            brain.Sensor3 = new ColorSensor();
            brain.Sensor1 = new TouchSensor();

            brain.Vehicle.LeftPort = MotorPort.OutA;    //
            brain.Vehicle.RightPort = MotorPort.OutD;   // initialise 'vehicle' motors
            brain.Vehicle.ReverseLeft = false;          //
            brain.Vehicle.ReverseRight = true;          //
            sbyte speed = 20;                           // robot's speed, will need changing

            ConsoleKeyInfo quitKey;                         //
            Console.WriteLine("Press Q to exit program");   // set up exit clause

            do
            {
                quitKey = Console.ReadKey(true);
                bool mode;
                Thread swapMode = new Thread (checkButton); // (checkButton(brain,mode));
                
            } while (quitKey.Key != ConsoleKey.Q);
        }
        catch(Exception e) {
            Console.WriteLine("Error: "+ e.Message);
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
    }      
}