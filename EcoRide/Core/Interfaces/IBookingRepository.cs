using EcoRide.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Interfaces
{
    public interface IBookingRepository
    {
        Task MarkAsPaid(string bookingId);
        Task DeleteByVehicle(string vehicleId);
    }
}
