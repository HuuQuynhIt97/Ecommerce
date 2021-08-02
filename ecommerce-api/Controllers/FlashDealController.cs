using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class FlashDealController : ApiController
    {
        private readonly IFlashDealService _flashDealService;

        public FlashDealController(IFlashDealService flashDealService)
        {
            _flashDealService = flashDealService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(FlashDealDto model)
        {
            return Ok(await _flashDealService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(FlashDealDto model)
        {
            return Ok(await _flashDealService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _flashDealService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _flashDealService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _flashDealService.Delete(id.ToInt()));
        }
    }
}