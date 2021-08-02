using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using ecommerce_api.Helpers.Params;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class CustomerProductController : ApiController
    {
        private readonly ICustomerProductService _customerProductService;

        public CustomerProductController(ICustomerProductService customerProductService)
        {
            _customerProductService = customerProductService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CustomerProductDto model)
        {
            return Ok(await _customerProductService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CustomerProductDto model)
        {
            return Ok(await _customerProductService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _customerProductService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId, string categoryId, string subCategoryId, string subSubCategoryId, string brandId)
        {
            var param = new CustomerProductParam
            {
                id = id.ToInt(),
                userId = userId.ToInt(),
                categoryId = categoryId.ToInt(),
                subCategoryId = subCategoryId.ToInt(),
                subSubCategoryId = subSubCategoryId.ToInt(),
                brandId = brandId.ToInt()
            };
            return Ok(await _customerProductService.GetById(param));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId, string categoryId, string subCategoryId, string subSubCategoryId, string brandId)
        {
            var param = new CustomerProductParam
            {
                id = id.ToInt(),
                userId = userId.ToInt(),
                categoryId = categoryId.ToInt(),
                subCategoryId = subCategoryId.ToInt(),
                subSubCategoryId = subSubCategoryId.ToInt(),
                brandId = brandId.ToInt()
            };
            return Ok(await _customerProductService.Delete(param));
        }
    }
}