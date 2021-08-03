using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class TicketController : ApiController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TicketDto model)
        {
            return Ok(await _ticketService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(TicketDto model)
        {
            return Ok(await _ticketService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _ticketService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId)
        {
            return Ok(await _ticketService.GetById(id.ToInt(), userId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            return Ok(await _ticketService.Delete(id.ToInt(), userId.ToInt()));
        }
    }
}