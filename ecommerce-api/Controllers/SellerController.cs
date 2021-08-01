using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class SellerController : ApiController
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(SellerDto model)
        {
            return Ok(await _sellerService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(SellerDto model)
        {
            return Ok(await _sellerService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _sellerService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId)
        {
            return Ok(await _sellerService.GetById(id.ToInt(), userId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            return Ok(await _sellerService.Delete(id.ToInt(), userId.ToInt()));
        }
    }
}