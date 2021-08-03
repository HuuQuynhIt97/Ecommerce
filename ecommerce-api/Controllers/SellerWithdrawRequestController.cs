using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class SellerWithdrawRequestController : ApiController
    {
        private readonly ISellerWithdrawRequestService _sellerWithdrawRequestService;

        public SellerWithdrawRequestController(ISellerWithdrawRequestService sellerWithdrawRequestService)
        {
            _sellerWithdrawRequestService = sellerWithdrawRequestService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(SellerWithdrawRequestDto model)
        {
            return Ok(await _sellerWithdrawRequestService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(SellerWithdrawRequestDto model)
        {
            return Ok(await _sellerWithdrawRequestService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _sellerWithdrawRequestService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId)
        {
            return Ok(await _sellerWithdrawRequestService.GetById(id.ToInt(), userId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            return Ok(await _sellerWithdrawRequestService.Delete(id.ToInt(), userId.ToInt()));
        }
    }
}