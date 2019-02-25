using GoPay.Model.Payment;
using GoPay.Model.Payments;
using Microsoft.Extensions.Logging;
using System;

namespace GoPay.Service
{
    public interface IRecurrenceService
    {
        long CreatePaidOnDemandPayment(GPConnector connector);

        void NextPayment(GPConnector connector, long IdOfPaidOnDemandPayment, NextPayment nextPayment);
    }

    public class RecurrenceService : IRecurrenceService
    {
        private readonly ILogger<RecurrenceService> _logger;

        public RecurrenceService(ILogger<RecurrenceService> logger)
        {
            this._logger = logger;
        }

        public long CreatePaidOnDemandPayment(GPConnector connector)
        {
            var recurrence = new Recurrence()
            {
                Cycle = RecurrenceCycle.ON_DEMAND,
            };

            var payment = new BasePayment();
            payment.Recurrence = recurrence;

            var result = connector.CreatePayment(payment);
            return result.Id;
        }

        public void NextPayment(GPConnector connector, long IdOfPaidOnDemandPayment, NextPayment nextPayment)
        {
            connector.CreateRecurrentPayment(IdOfPaidOnDemandPayment, nextPayment);
        }
    }
}
