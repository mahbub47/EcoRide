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

        public VehicleManager(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> CreateVehicle(string plateNo, string type)
        {
            var vehicle = VehicleFactory.CreateVehicle(type, Guid.NewGuid().ToString(), plateNo, true);
            await _vehicleRepository.AddAsync(vehicle);
            var createdVehicle = await _vehicleRepository.GetByIdAsync(vehicle.Id);
            return createdVehicle;
        }

        public async Task<List<Vehicle>> GetVehicles()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return vehicles.ToList();
        }

        public async Task<List<Vehicle>> GetAvailableVehicles()
        {
            var vehicles = await _vehicleRepository.GetAvailableVehicle();
            return vehicles.ToList();
        }

        public async Task<List<Vehicle>> GetAvailableVehiclesByType(string type)
        {
            var vehicles = await _vehicleRepository.GetAvailableVehicleByType(type);
            return vehicles.ToList();
        }
    }
}
