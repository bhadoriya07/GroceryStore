using BLL.Interface;
using DomainModelLayer;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace newGrocerApp.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("{productId}")]
        public IActionResult PostReview(int productId, [FromBody] PostReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedReview = _reviewService.PostReview(productId, reviewDto.Content, reviewDto.Name);
            return Ok(addedReview);
        }

        [HttpGet("{productId}")]
        public IActionResult GetReviews(int productId)
        {
            var reviews = _reviewService.GetReviewsByProduct(productId);
            return Ok(reviews);
        }
    }

}
