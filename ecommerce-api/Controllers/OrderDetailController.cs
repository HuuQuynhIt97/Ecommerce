using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class OrderDetailController : ApiController
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderDetailDto model)
        {
            return Ok(await _orderDetailService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(OrderDetailDto model)
        {
            return Ok(await _orderDetailService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _orderDetailService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id, int orderId, int sellerId, int productId)
        {
            return Ok(await _orderDetailService.GetById(id.ToInt(), orderId.ToInt(), productId.ToInt(), sellerId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id, int orderId, int sellerId, int productId)
        {
            return Ok(await _orderDetailService.Delete(id.ToInt(), orderId.ToInt(), productId.ToInt(), sellerId.ToInt()));
        }
    }
}