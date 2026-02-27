using EcoRide.Core.Models.Base;
using EcoRide.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Interfaces
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAvailableVehicle();
        Task<IEnumerable<Vehicle>> GetAvailableVehicleByType(string type);
        Task MarkAsBookedAsync(string id);
        Task MarkAsAvailableAsync(string id);
    }
}
