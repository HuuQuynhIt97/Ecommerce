using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class WishlistController :ApiController
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(WishlistDto model)
        {
            return Ok(await _wishlistService.Create(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param)
        {
            return Ok(await _wishlistService.GetDataWithPagination(param));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string productId, string userId)
        {
            return Ok(await _wishlistService.Delete(id.ToInt(), productId.ToInt(), userId.ToInt()));
        }
    }
}