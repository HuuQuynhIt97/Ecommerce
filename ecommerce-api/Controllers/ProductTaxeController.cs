using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class ProductTaxeController : ApiController
    {
        private readonly IProductTaxeService _productTaxeService;

        public ProductTaxeController(IProductTaxeService productTaxeService)
        {
            _productTaxeService = productTaxeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductTaxeDto model)
        {
            return Ok(await _productTaxeService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ProductTaxeDto model)
        {
            return Ok(await _productTaxeService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _productTaxeService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string productId, string taxeId)
        {
            return Ok(await _productTaxeService.GetById(id.ToInt(), productId.ToInt(), taxeId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string productId, string taxeId)
        {
            return Ok(await _productTaxeService.Delete(id.ToInt(), productId.ToInt(), taxeId.ToInt()));
        }
    }
}