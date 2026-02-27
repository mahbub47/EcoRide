using EcoRide.Core.Models.Base;
using EcoRide.Core.Models.Entities;
using EcoRide.Data.Repositories;
using EcoRide.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Services
{
    public class VehicleManager
    {
        private readonly VehicleRepository _vehicleRepository;

        public VehicleManager()
        {
            _vehicleRepository = new VehicleRepository();
        }

        public Vehicle CreateVehicle(string plateNo, string type)
        {
            var vehicle = VehicleFactory.CreateVehicle(type, Guid.NewGuid().ToString(), plateNo, true);
            _vehicleRepository.AddAsync(vehicle).Wait();
            return vehicle;
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            return _vehicleRepository.GetAvailableVehicle().Result.ToList();
        }

        public List<Vehicle> GetAvailableVehiclesByType(string type)
        {
            return _vehicleRepository.GetAvailableVehicleByType(type).Result.ToList();
        }
    }
}
