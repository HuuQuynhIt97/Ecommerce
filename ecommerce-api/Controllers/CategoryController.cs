using System.Threading.Tasks;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CategoryDto model)
        {
            return Ok(await _categoryService.Create(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _categoryService.GetDataWithPagination(param, text));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _categoryService.GetDataWithPagination(param, text, false));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] CategoryDto model)
        {
            return Ok(await _categoryService.Update(model));
        }
    }
}