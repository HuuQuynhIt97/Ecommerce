using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class LinkController : ApiController
    {
        private readonly ILinkService _linkService;

        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(LinkDto model)
        {
            return Ok(await _linkService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(LinkDto model)
        {
            return Ok(await _linkService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _linkService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _linkService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _linkService.Delete(id.ToInt()));
        }
    }
}