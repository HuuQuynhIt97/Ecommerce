using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class AddressController : ApiController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AddressesDto model)
        {
            return Ok(await _addressService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(AddressesDto model)
        {
            return Ok(await _addressService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _addressService.GetDataWithPagination(param, text));
        }

        [HttpGet("getByUser")]
        public async Task<IActionResult> GetByUserId([FromQuery] PaginationParams param, string userId)
        {
            return Ok(await _addressService.GetDataByUserID(param, userId.ToInt()));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId)
        {
            return Ok(await _addressService.GetDataByID(id.ToInt(), userId.ToInt()));
        }

        [HttpPut("setDefault")]
        public async Task<IActionResult> SetDefault(string id, string userId)
        {
            return Ok(await _addressService.SetDefault(id.ToInt(), userId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            return Ok(await _addressService.Delete(id.ToInt(), userId.ToInt()));
        }
    }
}