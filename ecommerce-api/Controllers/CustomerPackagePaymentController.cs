using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class CustomerPackagePaymentController : ApiController
    {
        private readonly ICustomerPackagePaymentService _customerPackagePaymentService;

        public CustomerPackagePaymentController(ICustomerPackagePaymentService customerPackagePaymentService)
        {
            _customerPackagePaymentService = customerPackagePaymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CustomerPackagePaymentDto model)
        {
            return Ok(await _customerPackagePaymentService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CustomerPackagePaymentDto model)
        {
            return Ok(await _customerPackagePaymentService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _customerPackagePaymentService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userID, string customerPackageId)
        {
            return Ok(await _customerPackagePaymentService.GetById(id.ToInt(), userID.ToInt(), customerPackageId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userID, string customerPackageId)
        {
            return Ok(await _customerPackagePaymentService.Delete(id.ToInt(), userID.ToInt(), customerPackageId.ToInt()));
        }
    }
}