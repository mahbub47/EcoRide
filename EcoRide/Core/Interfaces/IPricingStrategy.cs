using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Interfaces
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(decimal basePrice, int durationInHours);
    }
}
