using System;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"processing...{string.Join(",", args)}");
            await Task.Delay(1000);
            Console.WriteLine($"Thank you for playing Wing Commander!");
        }
        /*
        ⋊> ~/K/c/c/app on main ⨯ dotnet run                                                                                      06:28:12
        processing...
        Thank you for playing Wing Commander!
        ⋊> ~/K/c/c/app on main ⨯ dotnet run hello                                                                                06:28:26
        processing...hello
        Thank you for playing Wing Commander!
        ⋊> ~/K/c/c/app on main ⨯ dotnet run hello how are you                                                                    06:28:43
        processing...hello,how,are,you
        Thank you for playing Wing Commander!
        */
    }
}
