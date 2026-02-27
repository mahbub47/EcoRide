using EcoRide.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Strategies.PricingStrategies
{
    internal class RegularPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal basePrice, int durationInHours)
        {
            return basePrice * durationInHours;
        }
    }
}
