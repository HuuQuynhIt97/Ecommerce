using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class PageController : ApiController
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PageDto model)
        {
            return Ok(await _pageService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(PageDto model)
        {
            return Ok(await _pageService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _pageService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _pageService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _pageService.Delete(id.ToInt()));
        }
    }
}