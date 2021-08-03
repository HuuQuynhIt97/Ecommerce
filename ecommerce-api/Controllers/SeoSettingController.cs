using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class SeoSettingController : ApiController
    {
        private readonly ISeoSettingService _seoSettingService;

        public SeoSettingController(ISeoSettingService seoSettingService)
        {
            _seoSettingService = seoSettingService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(SeoSettingDto model)
        {
            return Ok(await _seoSettingService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(SeoSettingDto model)
        {
            return Ok(await _seoSettingService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _seoSettingService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _seoSettingService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _seoSettingService.Delete(id.ToInt()));
        }
    }
}