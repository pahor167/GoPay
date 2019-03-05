using GoPay.Model.Payment;
using GoPay.Model.Payments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoPay.Service
{
    public interface IRecurrenceService
    {
        Payment CreatePaidOnDemandPayment(GPConnector connector);

        Payment NextPayment(GPConnector connector, long IdOfPaidOnDemandPayment, NextPayment nextPayment);
    }

    public class RecurrenceService : IRecurrenceService
    {
        private readonly ILogger<RecurrenceService> _logger;

        public RecurrenceService(ILogger<RecurrenceService> logger)
        {
            this._logger = logger;
        }

        public Payment CreatePaidOnDemandPayment(GPConnector connector)
        {
            var recurrence = new Recurrence()
            {
                Cycle = RecurrenceCycle.ON_DEMAND,
                DateTo = DateTime.Now.AddYears(1),               
            };

            var payment = new BasePayment();
            payment.Target = new Target
            {
                GoId = 8156260189,
                Type = Target.TargetType.ACCOUNT
            };
            payment.Amount = 1;

            payment.Items = new List<OrderItem>
            {
                new OrderItem
                {
                    Amount = 1,
                    Count = 1,
                    Name = "Iniciační opakovaná platba",
                }
            };

            payment.Callback = new Callback
            {
                NotificationUrl = "http://www.test.cz",
                ReturnUrl = "http://www.test.cz"
            };

            payment.Recurrence = recurrence;

            payment.Payer = new Payer
            {
                Contact = new PayerContact
                {
                    Email = "test@test.com"
                },
                DefaultPaymentInstrument = PaymentInstrument.PAYMENT_CARD
            };

            var result = connector.CreatePayment(payment);
            return result;
        }

        public Payment NextPayment(GPConnector connector, long IdOfPaidOnDemandPayment, NextPayment nextPayment)
        {
            nextPayment.Items = new List<OrderItem>
            {
                new OrderItem
                {
                    Amount = nextPayment.Amount,
                    Count = 1,
                    Name = "Opakovaná platba",
                }
            };

            return connector.CreateRecurrentPayment(IdOfPaidOnDemandPayment, nextPayment);
        }
    }
}
