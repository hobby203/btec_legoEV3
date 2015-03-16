using System;
using MonoBrick.EV3;
//using MonoBrick.NXT;

public class Program{
    static void Main(string[] args){
        try {
            Console.WriteLine("Hey hey");
            Console.ReadKey();
            var brain = new Brick<Sensor, Sensor, Sensor, Sensor>("com6"); //set up brick
            brain.Connection.Open(); //connect to brick

            //brain.Sensor1 = new Sensor();
            //brain.Sensor1.ReadAsString();

            brain.Vehicle.LeftPort = MotorPort.OutA;    //
            brain.Vehicle.RightPort = MotorPort.OutD;   // initialise 'vehicle' motors
            brain.Vehicle.ReverseLeft = false;          //
            brain.Vehicle.ReverseRight = true;          //

            ConsoleKeyInfo quitKey;                         //
            Console.WriteLine("Press Q to exit program");   // set up exit clause

            do
            {

            } while (quitKey != ConsoleKey.Q);
        }
        catch(Exception e) {
            Console.WriteLine("Error: "+ e.Message);
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
    }

    static void initialise()
    {
        
    }
}