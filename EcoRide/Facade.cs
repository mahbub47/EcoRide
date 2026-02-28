using EcoRide.Core.Models.Base;
using EcoRide.Core.Models.Entities;
using EcoRide.Core.Services;
using EcoRide.Core.Strategies.PaymentStretagies;
using EcoRide.Core.Strategies.PricingStrategies;
using EcoRide.Data.Repositories;
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
            _userManager = new(new UserRepository());
            _vehicleManager = new(new VehicleRepository());
            _bookingManager = new(new BookingRepository(), new VehicleRepository(), new RegularPricingStrategy());
            _paymentManager = new(new BkashPaymentService(), new BookingRepository());
        }

        public async Task<User> RegisterUser(string name, string phone)
        {
            return await _userManager.CreateUser(name, phone);
        }

        public async Task<User> GetUser(string name)
        {
            return await _userManager.GetUserById(name);
        }

        public async Task<User> UpdateUser(string userId, string name, string phone)
        {
            return await _userManager.UpdateUserInformation(userId, name, phone);
        }

        public async Task DeleteUser(string userId)
        {
            await _userManager.DeleteUser(userId);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userManager.GetAllUsers();
        }

        public async Task<Vehicle> RegisterVehicle(string licensePlate, string type)
        {
            return await _vehicleManager.CreateVehicle(licensePlate, type);
        }

        public async Task<List<Vehicle>> GetVehicles()
        {
            return await _vehicleManager.GetVehicles();
        }

        public async Task<List<Vehicle>> GetAvailableVehicles()
        {
            return await _vehicleManager.GetAvailableVehicles();
        }

        public async Task<List<Vehicle>> GetAvailableVehiclesByType(string type)
        {
            return await _vehicleManager.GetAvailableVehiclesByType(type);
        }

        public async Task<Booking> CreateBooking(string userId, string vehicleId, int durationInHour)
        {
            return await _bookingManager.CreateBooking(userId, vehicleId, durationInHour);
        }

        public async Task UnbookVehicle(string vehicleId)
        {
            await _bookingManager.UnbookVehicle(vehicleId);
        }

        public async Task<string> PayForBooking(string bookingId, decimal amount)
        {
            return await _paymentManager.ProcessPayment(bookingId, amount);
        }

        public async Task DeleteVehicle(string vehicleId)
        {
            var vehicle = (await _vehicleManager.GetVehicles()).Find(v => v.Id == vehicleId);
            if (vehicle != null)
            {
                await _bookingManager.UnbookVehicle(vehicleId);
            }
        }
    }
}
