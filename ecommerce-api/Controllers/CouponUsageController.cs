using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class CouponUsageController : ApiController
    {
        private readonly ICouPonUsageService _couponUsageService;

        public CouponUsageController(ICouPonUsageService couponUsageService)
        {
            _couponUsageService = couponUsageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CouponUsageDto model)
        {
            return Ok(await _couponUsageService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CouponUsageDto model)
        {
            return Ok(await _couponUsageService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _couponUsageService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId, string couponId)
        {
            return Ok(await _couponUsageService.GetById(id.ToInt(), userId.ToInt(), couponId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId, string couponId)
        {
            return Ok(await _couponUsageService.Delete(id.ToInt(), userId.ToInt(), couponId.ToInt()));
        }
    }
}