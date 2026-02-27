using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Strategies
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(decimal basePrice, int durationInHours);
    }
}
