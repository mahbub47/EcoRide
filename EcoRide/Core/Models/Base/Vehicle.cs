using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Models.Base
{
    public abstract class Vehicle
    {
        public string Id { get; set; }
        public string PlateNumber { get; set; }
        public bool IsAvailable { get; set; }
        public decimal BasePrice { get; set; }

        public Vehicle(string id, string plateNumber, bool isAvailable, decimal basePrice)
        {
            Id = id;
            PlateNumber = plateNumber;
            IsAvailable = isAvailable;
            BasePrice = basePrice;
        }
    }
}
