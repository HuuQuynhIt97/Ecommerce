using System.Threading.Tasks;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    public class ReviewController : ApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Review(ReviewDto model)
        {
            return Ok(await _reviewService.Review(model));
        }

        [HttpGet]
        public async Task<IActionResult> CountView()
        {
            return Ok(await _reviewService.CountView());
        }
    }
}