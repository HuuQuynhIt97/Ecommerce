using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class ColorController : ApiController
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ColorDto model)
        {
            return Ok(await _colorService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ColorDto model)
        {
            return Ok(await _colorService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _colorService.GetDataWithPagination(param, text));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _colorService.Delete(id.ToInt()));
        }
    }
}