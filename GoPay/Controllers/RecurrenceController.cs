using AutoMapper;
using GoPay.Factories;
using GoPay.Model.Payments;
using GoPay.Models;
using GoPay.Service;
using Microsoft.AspNetCore.Mvc;

namespace GoPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurrenceController : ControllerBase
    {
        private readonly IConnectorFactory _connectorFactory;
        private readonly IRecurrenceService _recurrenceService;
        private readonly IMapper _mapper;

        public RecurrenceController(IRecurrenceService recurrenceService, IConnectorFactory connectorFactory, IMapper mapper)
        {
            this._connectorFactory = connectorFactory;
            this._recurrenceService = recurrenceService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Creates paid on demand payment
        /// </summary>
        /// <returns>Id of paid on deman payment</returns>
        /// <response code="200">Returns id of newly created paid on deman payment</response>
        /// <response code="500">Creation of paid on demand payment was not succesfull</response>     
        [HttpPost]
        public long CreatePaidOndemandPayment([FromBody]CreateRecurrenceModel model)
        {           
            return this._recurrenceService.CreatePaidOnDemandPayment(_connectorFactory.Create(model));            
        }

        /// <summary>
        /// Creates new client's payment based on paid on demand payment id
        /// </summary>
        /// <returns>Id of paid on demand payment</returns>
        /// <response code="200">Payment was successfull</response>
        /// <response code="500">Payment was not succesfull</response>     
        [HttpPost("{IdOfPaidOnDemandPayment}")]
        public void NextPayment(long IdOfPaidOnDemandPayment, [FromBody]NextPaymentModel model)
        {
            this._recurrenceService.NextPayment(_connectorFactory.Create(model),
                                                IdOfPaidOnDemandPayment,
                                                _mapper.Map<NextPayment>(model));           
        }
    }
}