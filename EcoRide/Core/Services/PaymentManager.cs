using EcoRide.Core.Interfaces;
using EcoRide.Core.Models.Entities;
using EcoRide.Core.Strategies.PaymentStretagies;
using EcoRide.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Services
{
    public class PaymentManager
    {
        private IPaymentService _paymentService;

        public PaymentManager()
        {
            _paymentService = new BkashPaymentService();
        }

        public void ProcessPayment(string bookingId, decimal amount)
        {
            var booking = new BookingRepository().GetByIdAsync(bookingId).Result;
            var isSuccess = _paymentService.ProcessPayment(booking.UserId, amount);
            if (isSuccess)
            {
                Console.WriteLine("Payment processed successfully.");
                new BookingRepository().MarkAsPaid(bookingId).Wait();
            }
            else
            {
                Console.WriteLine("Payment failed.");
            }
        }
    }
}
