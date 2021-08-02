using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class BrandController : ApiController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(BrandDto model)
        {
            return Ok(await _brandService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(BrandDto model)
        {
            return Ok(await _brandService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _brandService.GetDataWithPagination(param, text));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _brandService.Delete(id.ToInt()));
        }
    }
}