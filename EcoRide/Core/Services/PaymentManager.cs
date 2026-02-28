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
        private BookingRepository _bookingRepository;

        public PaymentManager(IPaymentService paymentService, BookingRepository bookingRepository)
        {
            _paymentService = paymentService;
            _bookingRepository = bookingRepository;
        }

        public async Task<string> ProcessPayment(string bookingId, decimal amount)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if(amount != booking.TotalPrice)
            {
                return "Payment amount does not match the booking total.";
            }
            var isSuccess = _paymentService.ProcessPayment(booking.UserId, amount);
            if (isSuccess)
            {
                new BookingRepository().MarkAsPaid(bookingId).Wait();
                return "Payment processed successfully.";
            }
            else
            {
                return "Payment failed.";
            }
        }
    }
}
