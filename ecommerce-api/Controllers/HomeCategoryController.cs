using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class HomeCategoryController : ApiController
    {
        private readonly IHomeCategoryService _homeCategoryService;

        public HomeCategoryController(IHomeCategoryService homeCategoryService)
        {
            _homeCategoryService = homeCategoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(HomeCategoryDto model)
        {
            return Ok(await _homeCategoryService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(HomeCategoryDto model)
        {
            return Ok(await _homeCategoryService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _homeCategoryService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _homeCategoryService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _homeCategoryService.Delete(id.ToInt()));
        }
    }
}