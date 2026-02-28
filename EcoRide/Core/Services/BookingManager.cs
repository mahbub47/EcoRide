using EcoRide.Core.Interfaces;
using EcoRide.Core.Models.Base;
using EcoRide.Core.Models.Entities;
using EcoRide.Core.Strategies;
using EcoRide.Core.Strategies.PricingStrategies;
using EcoRide.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Services
{
    public class BookingManager
    {
        private readonly BookingRepository _bookingRepository;
        private readonly VehicleRepository _vehicleRepository;
        private IPricingStrategy _pricingStrategy;

        public BookingManager(BookingRepository bookingRepository, VehicleRepository vehicleRepository, IPricingStrategy pricingStrategy)
        {
            _bookingRepository = bookingRepository;
            _vehicleRepository = vehicleRepository;
            _pricingStrategy = pricingStrategy;
        }

        public void SetPricingStrategy(IPricingStrategy pricingStrategy)
        {
            _pricingStrategy = pricingStrategy;
        }

        public async Task<Booking> CreateBooking(string userId, string vehicleId, int durationInHour)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            var totalPrice = _pricingStrategy.CalculatePrice(vehicle.BasePrice, durationInHour);
            var booking = new Booking(Guid.NewGuid().ToString(), userId, vehicleId, durationInHour, totalPrice, false);
            await _vehicleRepository.MarkAsBookedAsync(vehicleId);
            await _bookingRepository.AddAsync(booking);
            return booking;
        }

        public async Task UnbookVehicle(string vehicleId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            await _vehicleRepository.MarkAsAvailableAsync(vehicleId);
            await _bookingRepository.DeleteByVehicle(vehicleId);
        }
    }
}
