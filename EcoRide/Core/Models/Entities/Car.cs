using EcoRide.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Models.Entities
{
    public class Car : Vehicle
    {
        public Car(string id, string plateNumber, bool isAvailable) : base(id, plateNumber, isAvailable, 150.00m) { }
    }
}
