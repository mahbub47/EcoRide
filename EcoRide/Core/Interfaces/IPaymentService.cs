using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Interfaces
{
    public interface IPaymentService
    {
        bool ProcessPayment(string userId, decimal amount);
    }
}
