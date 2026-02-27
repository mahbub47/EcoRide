using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.ConsoleApp
{
    public static class ConsoleUtils
    {
        public static void RegisterUser(Facade system)
        {
            Console.WriteLine("Enter user name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter user phone:");
            string phone = Console.ReadLine();
            var user = system.RegisterUser(name, phone);
            Console.WriteLine($"User registered with ID: {user.Id}");
        }

        public static void RegisterVehicle(Facade system)
        {
            Console.WriteLine("Enter vehicle license plate:");
            string licensePlate = Console.ReadLine();
            Console.WriteLine("Select vehicle type 1.Car 2.Bike");
            string selectedType = Console.ReadLine();
            string type = selectedType switch
            {
                "1" => "Car",
                "2" => "Bike",
                _ => throw new ArgumentException("Invalid vehicle type selected.")
            };
            var vehicle = system.RegisterVehicle(licensePlate, type);
            Console.WriteLine($"Vehicle registered with ID: {vehicle.Id}");
        }

        public static void DisplayAllAvailableVehicles(Facade system)
        {
            var vehicles = system.GetAvailableVehicles();
            if (vehicles.Count == 0)
            {
                Console.WriteLine("No available vehicles.");
                return;
            }
            Console.WriteLine("All available vehicles:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"ID: {vehicle.Id}, Type: {vehicle.GetType().Name},Plate: {vehicle.PlateNumber}, Base Price: {vehicle.BasePrice}");
            }
        }

        public static void DisplayAvailableVehiclesByType(Facade system)
        {
            Console.WriteLine("Select vehicle type 1.Car 2.Bike");
            string selectedType = Console.ReadLine();
            string type = selectedType switch
            {
                "1" => "Car",
                "2" => "Bike",
                _ => throw new ArgumentException("Invalid vehicle type selected.")
            };
            var vehicles = system.GetAvailableVehiclesByType(type);
            if (vehicles.Count == 0)
            {
                Console.WriteLine($"No available {type.ToLower()}s.");
                return;
            }
            Console.WriteLine($"Available {type.ToLower()}s:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"ID: {vehicle.Id}, Type: {vehicle.GetType().Name}, Plate: {vehicle.PlateNumber}, Base Price: {vehicle.BasePrice}");
            }
        }

        public static void BookVehicle(Facade system)
        {
            Console.WriteLine("Enter user ID:");
            string userId = Console.ReadLine();
            Console.WriteLine("Enter vehicle ID:");
            string vehicleId = Console.ReadLine();
            Console.WriteLine("Enter duration in hours:");
            int duration = int.Parse(Console.ReadLine());
            var booking = system.CreateBooking(userId, vehicleId, duration);
            Console.WriteLine($"Booking created with ID: {booking.Id}, Total Price: {booking.TotalPrice}");
        }

        public static void PayForBooking(Facade system)
        {
            Console.WriteLine("Enter booking ID:");
            string bookingId = Console.ReadLine();
            Console.WriteLine("Enter payment amount:");
            decimal amount = decimal.Parse(Console.ReadLine());
            system.PayForBooking(bookingId, amount);
        }

        public static void DisplayUsers(Facade system)
        {
            var users = system.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No registered users.");
                return;
            }
            Console.WriteLine("Registered users:");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Phone: {user.Phone}");
            }
        }

        public static void DisplayUserInformation(Facade system)
        {
            Console.WriteLine("Enter user ID:");
            string userId = Console.ReadLine();
            var user = system.GetUser(userId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            Console.WriteLine($"User ID: {user.Id}, Name: {user.Name}, Phone: {user.Phone}");
        }

        public static void DisplayAllVehciles(Facade system)
        {
            var vehicles = system.GetVehicles();
            if (vehicles.Count == 0)
            {
                Console.WriteLine("No registered vehicles.");
                return;
            }
            Console.WriteLine("Registered vehicles:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"ID: {vehicle.Id}, Type: {vehicle.GetType().Name}, Plate: {vehicle.PlateNumber}, Base Price: {vehicle.BasePrice}");

            }
        }

        public static void UnbookVehicle(Facade system)
        {
            Console.WriteLine("Enter vehicle ID:");
            string vehicleId = Console.ReadLine();
            system.UnbookVehicle(vehicleId);
            Console.WriteLine("Vehicle unbooked successfully.");
        }

        public static void DeleteUser(Facade system)
        {
            Console.WriteLine("Enter user ID:");
            string userId = Console.ReadLine();
            system.DeleteUser(userId);
            Console.WriteLine("User deleted successfully.");
        }

        public static void DeleteVehicle(Facade system)
        {
            Console.WriteLine("Enter vehicle ID:");
            string vehicleId = Console.ReadLine();
            system.DeleteVehicle(vehicleId);
            Console.WriteLine("Vehicle deleted successfully.");
        }
    }
}
