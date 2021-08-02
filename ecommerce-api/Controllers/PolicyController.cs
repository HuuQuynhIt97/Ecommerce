using System.Threading.Tasks;
using CodeUtility;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class PolicyController : ApiController
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PolicyDto model)
        {
            return Ok(await _policyService.Create(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(PolicyDto model)
        {
            return Ok(await _policyService.Update(model));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataWithPagination([FromQuery] PaginationParams param, string text)
        {
            return Ok(await _policyService.GetDataWithPagination(param, text));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _policyService.Delete(id.ToInt()));
        }
    }
}