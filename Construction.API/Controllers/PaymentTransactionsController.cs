using Asp.Versioning;
using AutoMapper;
using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [Authorize]
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PaymentTransactionsController : ControllerBase
    {
        private readonly ILogger<PaymentTransactionsController> logger;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;

        public PaymentTransactionsController(
            ILogger<PaymentTransactionsController> logger,
            IMapper mapper,
            IPaymentService paymentService
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.paymentService = paymentService;
        }

        /// <summary>
        /// This Endpoint to get transaction by ID
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpGet("{transactionId}", Name = "GetTransactionById")]
        public async Task<ActionResult<PaymentTransactionDTO>>
            GetTransaction(int projectId, int transactionId)
        {
            try
            {
                var transaction = await paymentService
                    .FindPaymentTransactionById(projectId, transactionId);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to add transaction to project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="transactionDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult>
            AddTransaction(int projectId, [FromBody] PaymentTransactionDTO transactionDto)
        {
            try
            {
                var transaction = await paymentService
                    .AddTransactionAsync(projectId, transactionDto);

                return CreatedAtRoute("GetTransactionById",
                    new { projectId = transaction.Id },
                    mapper.Map<PaymentTransactionDTO>(transaction)
                    );
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
