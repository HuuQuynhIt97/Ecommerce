using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class ShopController : ApiController
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ShopDto model)
        {
            return Ok(await _shopService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ShopDto model)
        {
            return Ok(await _shopService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _shopService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId)
        {
            return Ok(await _shopService.GetById(id.ToInt(), userId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            return Ok(await _shopService.Delete(id.ToInt(), userId.ToInt()));
        }
    }
}