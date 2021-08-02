using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class BlogController : ApiController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(BlogDto model)
        {
            return Ok(await _blogService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(BlogDto model)
        {
            return Ok(await _blogService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination(PaginationParams param, string text)
        {
            return Ok(await _blogService.GetDataWithPagination(param, text));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string cateId)
        {
            return Ok(await _blogService.Delete(id.ToInt(), cateId.ToInt()));
        }
    }
}