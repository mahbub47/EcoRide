using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.ConsoleApp
{
    internal class ConsoleApp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("================== WELCOME TO ECORIDE =================");
            var system = new Facade();
            MenuHandler.DisplayMenu(system);
        }
    }
}
