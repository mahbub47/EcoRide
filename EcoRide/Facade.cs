using EcoRide.Core.Models.Base;
using EcoRide.Core.Models.Entities;
using EcoRide.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide
{
    public class Facade
    {
        private readonly UserManager _userManager;
        private readonly VehicleManager _vehicleManager;
        private readonly BookingManager _bookingManager;
        private readonly PaymentManager _paymentManager;

        public Facade()
        {
            _userManager = new();
            _vehicleManager = new();
            _bookingManager = new();
            _paymentManager = new();
        }

        public User RegisterUser(string name, string phone)
        {
            return _userManager.CreateUser(name, phone);
        }

        public User GetUser(string name)
        {
            return _userManager.GetUserById(name);
        }

        public User UpdateUser(string userId, string name, string phone)
        {
            return _userManager.UpdateUserInformation(userId, name, phone);
        }

        public void DeleteUser(string userId)
        {
            _userManager.DeleteUser(userId);
        }

        public List<User> GetUsers()
        {
            return _userManager.GetAllUsers();
        }

        public Vehicle RegisterVehicle(string licensePlate, string type)
        {
            return _vehicleManager.CreateVehicle(licensePlate, type);
        }

        public List<Vehicle> GetVehicles()
        {
            return _vehicleManager.GetVehicles();
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            return _vehicleManager.GetAvailableVehicles();
        }

        public List<Vehicle> GetAvailableVehiclesByType(string type)
        {
            return _vehicleManager.GetAvailableVehiclesByType(type);
        }

        public Booking CreateBooking(string userId, string vehicleId, int durationInHour)
        {
            return _bookingManager.CreateBooking(userId, vehicleId, durationInHour);
        }

        public void UnbookVehicle(string vehicleId)
        {
            _bookingManager.unbookVehicle(vehicleId);
        }

        public void PayForBooking(string bookingId, decimal amount)
        {
            _paymentManager.ProcessPayment(bookingId, amount);
        }

        public void DeleteVehicle(string vehicleId)
        {
            var vehicle = _vehicleManager.GetVehicles().Find(v => v.Id == vehicleId);
            if (vehicle != null)
            {
                _bookingManager.unbookVehicle(vehicleId);
            }
        }
    }
}
