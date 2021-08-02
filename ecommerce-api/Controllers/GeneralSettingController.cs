using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class GeneralSettingController : ApiController
    {
        private readonly IGeneralSettingService _generalSettingService;

        public GeneralSettingController(IGeneralSettingService generalSettingService)
        {
            _generalSettingService = generalSettingService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(GeneralSettingDto model)
        {
            return Ok(await _generalSettingService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(GeneralSettingDto model)
        {
            return Ok(await _generalSettingService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _generalSettingService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _generalSettingService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _generalSettingService.Delete(id.ToInt()));
        }
    }
}