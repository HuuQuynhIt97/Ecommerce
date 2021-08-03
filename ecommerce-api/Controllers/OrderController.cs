using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderDto model)
        {
            return Ok(await _orderService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(OrderDto model)
        {
            return Ok(await _orderService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _orderService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId, string guestId, string sellerId)
        {
            return Ok(await _orderService.GetById(id.ToInt(), userId.ToInt(), guestId.ToInt(), sellerId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId, string guestId, string sellerId)
        {
            return Ok(await _orderService.Delete(id.ToInt(), userId.ToInt(), guestId.ToInt(), sellerId.ToInt()));
        }
    }
}