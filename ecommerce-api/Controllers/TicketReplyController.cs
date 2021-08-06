using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class TicketReplyController : ApiController
    {
        private readonly ITicketReplyService _ticketReplyService;

        public TicketReplyController(ITicketReplyService ticketReplyService)
        {
            _ticketReplyService = ticketReplyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TicketReplyDto model)
        {
            return Ok(await _ticketReplyService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(TicketReplyDto model)
        {
            return Ok(await _ticketReplyService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _ticketReplyService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string ticketId, string userId)
        {
            return Ok(await _ticketReplyService.GetById(id.ToInt(), ticketId.ToInt(), userId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string ticketId, string userId)
        {
            return Ok(await _ticketReplyService.Delete(id.ToInt(), ticketId.ToInt(), userId.ToInt()));
        }
    }
}