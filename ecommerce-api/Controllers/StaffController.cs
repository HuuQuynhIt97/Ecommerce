using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class StaffController :ApiController
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(StaffDto model)
        {
            return Ok(await _staffService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(StaffDto model)
        {
            return Ok(await _staffService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _staffService.GetDataWithPagination(param, text));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id, string userId, string roleId)
        {
            return Ok(await _staffService.GetById(id.ToInt(), userId.ToInt(), roleId.ToInt()));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, string userId, string roleId)
        {
            return Ok(await _staffService.Delete(id.ToInt(), userId.ToInt(), roleId.ToInt()));
        }
    }
}