using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Strategies
{
    internal class RushHourPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal basePrice, int durationInHours)
        {
            return basePrice * durationInHours * 1.2m;
        }
    }
}
