using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = 17.76m;
            var s1 = $"interpolated: value={value,6:C}";
            var s2 = string.Format("interpolated: value={0,6:C}", value);
            Console.WriteLine(s1);
            Console.WriteLine(s2);
        }
        /*
        private static void Main (string[] args)
	    {
	    	decimal num = 17.76m;
	    	string value = string.Format ("interpolated: value={0,6:C}", num);
	    	string value2 = string.Format ("interpolated: value={0,6:C}", num);
	    	Console.WriteLine (value);
	    	Console.WriteLine (value2);
	    }
        */
    }
}
