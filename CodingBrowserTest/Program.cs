using CodingBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodingBrowserTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Browser Starting...");
            var browser = Browser.Start(@"https://www.codingame.com");
            Console.WriteLine("Browser Statred");

            while (true)
            {
                Thread.Sleep(800);



            }
            Browser.Shutdown();

        }
    }
}
