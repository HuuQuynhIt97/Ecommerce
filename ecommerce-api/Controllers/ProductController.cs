using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductDto model)
        {
            return Ok(await _productService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ProductDto model)
        {
            return Ok(await _productService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _productService.GetDataWithPagination(param, text));
        }

        [HttpGet("item")]
        public async Task<IActionResult> GetById(string id, string cateId, string brandId)
        {
            return Ok(await _productService.GetById(id.ToInt(), cateId.ToInt(), brandId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string cateId, string brandId)
        {
            return Ok(await _productService.Delete(id.ToInt(), cateId.ToInt(), brandId.ToInt()));
        }
    }
}