using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class TaxeController : ApiController
    {
        private readonly ITaxeService _taxeService;

        public TaxeController(ITaxeService taxeService)
        {
            _taxeService = taxeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TaxeDto model)
        {
            return Ok(await _taxeService.Create(model));
        }

        [HttpPut("chageStatus")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            return Ok(await _taxeService.ChangeStatus(id.ToInt()));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(TaxeDto model)
        {
            return Ok(await _taxeService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _taxeService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _taxeService.GetById(id.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _taxeService.Delete(id.ToInt()));
        }
    }
}