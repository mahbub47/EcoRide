using EcoRide.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Strategies.PaymentStretagies
{
    internal class BkashPaymentService : IPaymentService
    {
        public bool ProcessPayment(string userId, decimal amount)
        {
            Console.WriteLine($"Processing Bkash payment for user {userId} with amount {amount}");
            return true;
        }
    }
}
