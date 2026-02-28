using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.ConsoleApp
{
    internal class ConsoleApp
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("================== WELCOME TO ECORIDE =================");
            var system = new Facade();
            await MenuHandler.DisplayMenu(system);
        }
    }
}
