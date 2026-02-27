using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide
{
    public class SystemDemo
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to EcoRide System Demo!");
            var system = new Facade();
            //var user1 = system.RegisterUser("Bob", "123-456-7810");
            //Console.WriteLine($"User registered: {user1.Name}, {user1.Phone}");
            //var user1 = system.UpdateUser("7ecb15b9-93ff-43cf-8859-c2b67337f5ae", "Bob", "123-456-7810");
            //system.DeleteUser("123");
            //var users = system.GetUsers();
            //foreach (var user in users)
            //{
            //    Console.WriteLine($"User: {user.Name}, {user.Phone}");
            //}
            //var vehicle1 = system.RegisterVehicle("FGH-456", "Bike");
            //Console.WriteLine($"Vehicle registered: {vehicle1.GetType().Name}, {vehicle1.PlateNumber}");
            var vehicles = system.GetAvailableVehicles();
            if (vehicles.Count == 0)
            {
                Console.WriteLine("No available vehicles.");
            }
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"Available Vehicle: {vehicle.GetType().Name}, {vehicle.PlateNumber}, Base Price: {vehicle.BasePrice}");
            }

            //var booking1 = system.CreateBooking("7ecb15b9-93ff-43cf-8859-c2b67337f5ae", "6fb5d798-eebc-422c-b280-794495ed988d", 2);
            //Console.WriteLine($"Booking created: UserId={booking1.UserId}, VehicleId={booking1.VehicleId}, Duration={booking1.DurationInHours}, TotalPrice={booking1.TotalPrice}");
        }
    }
}
