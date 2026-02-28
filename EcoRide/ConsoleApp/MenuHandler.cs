using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.ConsoleApp
{
    public class MenuHandler
    {
        public static async Task DisplayMenu(Facade system)
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
                Console.WriteLine("0. Exit");
                Console.WriteLine();

                Console.WriteLine("Type 'Exit' to exit");
                Console.Write("Your selection: ");

                int.TryParse(Console.ReadLine(), out int input);
                switch (input)
                {
                    case 1:
                        await ConsoleUtils.RegisterUser(system);
                        break;
                    case 2:
                        await ConsoleUtils.RegisterVehicle(system);
                        break;
                    case 3:
                        await ConsoleUtils.DisplayAllAvailableVehicles(system);
                        break;
                    case 4:
                        await ConsoleUtils.DisplayAvailableVehiclesByType(system);
                        break;
                    case 5:
                        await ConsoleUtils.BookVehicle(system);   
                        break;
                    case 6:
                        await ConsoleUtils.PayForBooking(system);
                        break;
                    case 7:
                        await ConsoleUtils.DisplayUsers(system);
                        break;
                    case 8:
                        await ConsoleUtils.DisplayUserInformation(system);
                        break;
                    case 9:
                        await ConsoleUtils.DisplayAllVehicles(system);
                        break;
                    case 10:
                        await ConsoleUtils.UnbookVehicle(system);
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            }
        }
    }
}
