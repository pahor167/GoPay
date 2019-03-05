using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GoPay.Factories;
using GoPay.Model.Payments;
using GoPay.Models;
using GoPay.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConnectorFactory _connectorFactory;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, IConnectorFactory connectorFactory, IMapper mapper)
        {
            this._connectorFactory = connectorFactory;
            this._paymentService = paymentService;
            this._mapper = mapper;
        }

        [HttpPost("{paymentId}")]
        public Payment PaymentStatus(long paymentId, [FromBody]PaymentStatusModel model)
        {
            return this._paymentService.PaymentStatus(_connectorFactory.Create(model), paymentId);
        }
    }
}