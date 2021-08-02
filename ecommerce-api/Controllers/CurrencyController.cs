using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class CurrencyController : ApiController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CurrencyDto model)
        {
            return Ok(await _currencyService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CurrencyDto model)
        {
            return Ok(await _currencyService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _currencyService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _currencyService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _currencyService.Delete(id.ToInt()));
        }
    }
}