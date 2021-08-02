using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class ProductStockController : ApiController
    {
        private readonly IProductStockService _productStockService;

        public ProductStockController(IProductStockService productStockService)
        {
            _productStockService = productStockService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductStockDto model)
        {
            return Ok(await _productStockService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ProductStockDto model)
        {
            return Ok(await _productStockService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _productStockService.GetDataWithpagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string productId)
        {
            return Ok(await _productStockService.GetById(id.ToInt(), productId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string productId)
        {
            return Ok(await _productStockService.Delete(id.ToInt(), productId.ToInt()));
        }
    }
}