using EcoRide.Core.Models.Entities;
using EcoRide.Core.Strategies;
using EcoRide.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Services
{
    public class BookingManager
    {
        private readonly BookingRepository _bookingRepository;
        private IPricingStrategy _pricingStrategy;

        public BookingManager()
        {
            _bookingRepository = new BookingRepository();
            _pricingStrategy = new RegularPricingStrategy();
        }

        public void SetPricingStrategy(IPricingStrategy pricingStrategy)
        {
            _pricingStrategy = pricingStrategy;
        }

        public Booking CreateBooking(string userId, string vehicleId, int durationInHour)
        {
            var vehicle = new VehicleRepository().GetByIdAsync(vehicleId).Result;
            var totalPrice = _pricingStrategy.CalculatePrice(vehicle.BasePrice, durationInHour);
            var booking = new Booking(userId, vehicleId, durationInHour, totalPrice);
            new VehicleRepository().MarkAsBookedAsync(vehicleId).Wait();
            _bookingRepository.AddAsync(booking).Wait();
            return booking;
        }

        public void unbookVehicle(string vehicleId)
        {
            new VehicleRepository().MarkAsAvailableAsync(vehicleId).Wait();
        }
    }
}
