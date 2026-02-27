using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Interfaces
{
    public interface IPaymentService
    {
        void ProcessPayment(string userId, decimal amount);
    }
}
