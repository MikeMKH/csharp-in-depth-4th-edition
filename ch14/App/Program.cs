using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;
            if (hour > 4)
            {
                int minute = now.Minute;
                PrintMessage("wake up");
                
                void PrintMessage(string message) => Console.WriteLine($"{hour}:{minute} {message}");
            }
        }
    }
    /*
    private static void Main (string[] args)
    {
    	DateTime now = DateTime.Now;
    	int hour = now.Hour;
    	int minute;
    	if (hour > 4) {
    		minute = now.Minute;
    		PrintMessage ("wake up");
    	}
    	void PrintMessage (string message)
    	{
    		Console.WriteLine ($"{hour}:{minute} {message}");
    	}
    }
    */
}
