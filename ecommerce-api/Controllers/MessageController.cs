using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(MessageDto model)
        {
            return Ok(await _messageService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(MessageDto model)
        {
            return Ok(await _messageService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _messageService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string conversationId, string userId)
        {
            return Ok(await _messageService.GetById(id.ToInt(), conversationId.ToInt(), userId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string conversationId, string userId)
        {
            return Ok(await _messageService.Delete(id.ToInt(), conversationId.ToInt(), userId.ToInt()));
        }
    }
}