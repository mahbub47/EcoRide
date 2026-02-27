using EcoRide.Core.Models.Base;
using EcoRide.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Factory
{
    public static class VehicleFactory
    {
        public static Vehicle CreateVehicle(string type, string id, string plateNumber, bool isAvailable)
        {
            return type.ToLower() switch
            {
                "car" => new Car(id, plateNumber, isAvailable),
                "bike" => new Bike(id, plateNumber, isAvailable),
                _ => throw new ArgumentException("Invalid vehicle type")
            };
        }
    }
}
