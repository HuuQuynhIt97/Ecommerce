using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class CityController : ApiController
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CityDto model)
        {
            return Ok(await _cityService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CityDto model)
        {
            return Ok(await _cityService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _cityService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string countryId)
        {
            return Ok(await _cityService.GetByID(id.ToInt(), countryId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string countryId)
        {
            return Ok(await _cityService.Delete(id.ToInt(), countryId.ToInt()));
        }
    }
}