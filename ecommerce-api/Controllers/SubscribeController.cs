using System.Threading.Tasks;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class SubscribeController : ApiController
    {
        private readonly ISubscribeService _subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscribeDto model)
        {
            return Ok(await _subscribeService.Subscribe(model));
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountSubscribe()
        {
            return Ok(await _subscribeService.CountSubscribe());
        }
    }
}