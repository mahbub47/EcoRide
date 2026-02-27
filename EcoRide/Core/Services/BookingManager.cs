using EcoRide.Core.Models.Entities;
using EcoRide.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Services
{
    public class BookingManager
    {
        private readonly BookingRepository _bookingRepository;

        public BookingManager()
        {
            _bookingRepository = new BookingRepository();
        }

        public Booking CreateBooking(string userId, string vehicleId, int durationInHour)
        {
            var booking = new Booking(userId, vehicleId, durationInHour, 100); 
            _bookingRepository.AddAsync(booking).Wait();
            return booking;
        }
    }
}
