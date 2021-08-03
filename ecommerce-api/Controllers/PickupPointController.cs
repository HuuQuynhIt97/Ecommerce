using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class PickupPointController : ApiController
    {
        private readonly IPickupPointService _pickupPointService;

        public PickupPointController(IPickupPointService pickupPointService)
        {
            _pickupPointService = pickupPointService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PickupPointDto model)
        {
            return Ok(await _pickupPointService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(PickupPointDto model)
        {
            return Ok(await _pickupPointService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _pickupPointService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string staffId)
        {
            return Ok(await _pickupPointService.GetById(id.ToInt(), staffId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string staffId)
        {
            return Ok(await _pickupPointService.Delete(id.ToInt(), staffId.ToInt()));
        }
    }
}