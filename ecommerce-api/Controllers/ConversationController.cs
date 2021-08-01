using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class ConversationController : ApiController
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ConversationDto model)
        {
            return Ok(await _conversationService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ConversationDto model)
        {
            return Ok(await _conversationService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _conversationService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string senderId, string receiverId)
        {
            return Ok(await _conversationService.GetById(id.ToInt(), senderId.ToInt(), receiverId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string senderId, string receiverId)
        {
            return Ok(await _conversationService.Delete(id.ToInt(), senderId.ToInt(), receiverId.ToInt()));
        }
    }
}