using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Models.Entities
{
    public class Booking
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string VehicleId { get; set; }
        public int DurationInHours { get; set; }
        public decimal TotalPrice { get; set; }

        public Booking(string userId, string vehicleId, int durationInHours, decimal totalPrice)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            VehicleId = vehicleId;
            DurationInHours = durationInHours;
            TotalPrice = totalPrice;
        }
    }
}