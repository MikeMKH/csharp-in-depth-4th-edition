using System;
using System.Globalization;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = 17.76m;
            var s1 = $"interpolated: value={value,6:C}";
            var s2 = string.Format("format: value={0,6:C}", value);
            var s3 = ((FormattableString) $"formattable string: value={value,6:C}")
              .ToString(CultureInfo.GetCultureInfo("en-GB"));
            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
        }
        /*
	    private static void Main (string[] args)
	    {
	    	decimal num = 17.76m;
	    	string value = string.Format ("interpolated: value={0,6:C}", num);
	    	string value2 = string.Format ("format: value={0,6:C}", num);
	    	string value3 = FormattableStringFactory.Create ("formattable string: value={0,6:C}", num).ToString (CultureInfo.GetCultureInfo ("en-GB"));
	    	Console.WriteLine (value);
	    	Console.WriteLine (value2);
	    	Console.WriteLine (value3);
	    }
        */
    }
}
