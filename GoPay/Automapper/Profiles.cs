using AutoMapper;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Models;

namespace GoPay.Automapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<NextPaymentModel, NextPayment>();
            CreateMap<GpCurrency, Currency>();
        }
    }
}
