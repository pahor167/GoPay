using GoPay.Model.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoPay.Service
{
    public interface IPaymentService
    {
        Payment PaymentStatus(GPConnector connector, long paymentId);
    }

    public class PaymentService: IPaymentService
    {
        public Payment PaymentStatus(GPConnector connector, long paymentId)
        {
            var payment = connector.PaymentStatus(paymentId);
            return payment;
        }
    }
}
