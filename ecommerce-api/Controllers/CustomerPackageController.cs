using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class CustomerPackageController : ApiController
    {
        private readonly ICustomerPackageService _customerPackageService;

        public CustomerPackageController(ICustomerPackageService customerPackageService)
        {
            _customerPackageService = customerPackageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CustomerPackageDto model)
        {
            return Ok(await _customerPackageService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CustomerPackageDto model)
        {
            return Ok(await _customerPackageService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _customerPackageService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _customerPackageService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _customerPackageService.Delete(id.ToInt()));
        }
    }
}