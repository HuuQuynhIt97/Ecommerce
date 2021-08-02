using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class CommissionHistoryController : ApiController
    {
        private readonly ICommissionHistoryService _commissionHistoryService;

        public CommissionHistoryController(ICommissionHistoryService commissionHistoryService)
        {
            _commissionHistoryService = commissionHistoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CommissionHistoryDto model)
        {
            return Ok(await _commissionHistoryService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CommissionHistoryDto model)
        {
            return Ok(await _commissionHistoryService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _commissionHistoryService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string orderId, string orderDetailId, string sellerId)
        {
            return Ok(await _commissionHistoryService.GetById(id.ToInt(), orderId.ToInt(), orderDetailId.ToInt(), sellerId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string orderId, string orderDetailId, string sellerId)
        {
            return Ok(await _commissionHistoryService.Delete(id.ToInt(), orderId.ToInt(), orderDetailId.ToInt(), sellerId.ToInt()));
        }
    }
}