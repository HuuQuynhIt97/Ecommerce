using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class BlogCategoryController : ApiController
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Blog_categoryDto model)
        {
            return Ok(await _blogCategoryService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Blog_categoryDto model)
        {
            return Ok(await _blogCategoryService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _blogCategoryService.GetDataWithPaination(param, text));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _blogCategoryService.Delete(id.ToInt()));
        }
    }
}