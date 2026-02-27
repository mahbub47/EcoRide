using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.ConsoleApp
{
    public class MenuHandler
    {
        public static void DisplayMenu(Facade system)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("============================= Main Menu =========================");
                Console.WriteLine();
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. Register Vehicle");
                Console.WriteLine("3. Display All Available Vehicles");
                Console.WriteLine("4. Display All Available Vehicles By Type");
                Console.WriteLine("5. Book vehicle");
                Console.WriteLine("6. Pay");
                Console.WriteLine("7. Display Users");
                Console.WriteLine("8. Display User Informations");
                Console.WriteLine("9. Display All Vehicles");
                Console.WriteLine("10. Unbook a vehicle");
                Console.WriteLine();

                Console.WriteLine("Type 'Exit' to exit");
                Console.Write("Your selection: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ConsoleUtils.RegisterUser(system);
                        break;
                    case "2":
                        ConsoleUtils.RegisterVehicle(system);
                        break;
                    case "3":
                        ConsoleUtils.DisplayAllAvailableVehicles(system);
                        break;
                    case "4":
                        ConsoleUtils.DisplayAvailableVehiclesByType(system);
                        break;
                    case "5":
                        ConsoleUtils.BookVehicle(system);
                        break;
                    case "6":
                        ConsoleUtils.PayForBooking(system);
                        break;
                    case "7":
                        ConsoleUtils.DisplayUsers(system);
                        break;
                    case "8":
                        ConsoleUtils.DisplayUserInformation(system);
                        break;
                    case "9":
                        ConsoleUtils.DisplayAllVehciles(system);
                        break;
                    case "10":
                        ConsoleUtils.UnbookVehicle(system);
                        break;
                    case "exit":
                        break;
                }
            }
        }
    }
}
