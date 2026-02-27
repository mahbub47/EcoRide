using EcoRide.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Models.Entities
{
    public class Bike : Vehicle
    {
        public Bike(string id, string plateNumber, bool isAvailable) : base(id, plateNumber, isAvailable, 100.00m) { }
    }
}
