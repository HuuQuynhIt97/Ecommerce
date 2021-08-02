using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class FlashDealProductController : ApiController
    {
        private readonly IFlashDealProductService _flashDealProductService;

        public FlashDealProductController(IFlashDealProductService flashDealProductService)
        {
            _flashDealProductService = flashDealProductService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(FlashDealProductDto model)
        {
            return Ok(await _flashDealProductService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(FlashDealProductDto model)
        {
            return Ok(await _flashDealProductService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _flashDealProductService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string flashDealId, string productId)
        {
            return Ok(await _flashDealProductService.GetById(id.ToInt(), flashDealId.ToInt(), productId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string flashDealId, string productId)
        {
            return Ok(await _flashDealProductService.Delete(id.ToInt(), flashDealId.ToInt(), productId.ToInt()));
        }
    }
}