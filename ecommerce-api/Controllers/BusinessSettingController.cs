using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class BusinessSettingController : ApiController
    {
        private readonly IBusinessSettingService _businessSettingService;

        public BusinessSettingController(IBusinessSettingService businessSettingService)
        {
            _businessSettingService = businessSettingService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Business_settingDto model)
        {
            return Ok(await _businessSettingService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Business_settingDto model)
        {
            return Ok(await _businessSettingService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _businessSettingService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _businessSettingService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _businessSettingService.Delete(id.ToInt()));
        }
    }
}