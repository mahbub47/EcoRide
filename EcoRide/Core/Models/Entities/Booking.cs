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

        public bool PaymentStatus { get; set; }

        public Booking(string id, string userId, string vehicleId, int durationInHours, decimal totalPrice, bool paymentStatus)
        {
            Id = id;
            UserId = userId;
            VehicleId = vehicleId;
            DurationInHours = durationInHours;
            PaymentStatus = paymentStatus;
            TotalPrice = totalPrice;
        }
    }
}