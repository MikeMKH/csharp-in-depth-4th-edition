using System;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintAndWait(TimeSpan.FromMilliseconds(100));
        }
        
        static async Task PrintAndWait(TimeSpan delay)
        {
            Console.WriteLine("before delays");
            await Task.Delay(delay);
            Console.WriteLine("between delays");
            await Task.Delay(delay);
            Console.WriteLine("after delays");
        }
    }
}
