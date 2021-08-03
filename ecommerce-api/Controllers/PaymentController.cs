using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PaymentDto model)
        {
            return Ok(await _paymentService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(PaymentDto model)
        {
            return Ok(await _paymentService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _paymentService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string sellerId)
        {
            return Ok(await _paymentService.GetById(id.ToInt(), sellerId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string sellerId)
        {
            return Ok(await _paymentService.Delete(id.ToInt(), sellerId.ToInt()));
        }
    }
}