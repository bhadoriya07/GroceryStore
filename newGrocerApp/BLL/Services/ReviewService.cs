using BLL.Interface;
using DAL.Interface;
using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Review PostReview(int productId, string reviewContent, string name)
        {
            var review = new Review
            {
                ProductId = productId,
                Content = reviewContent,
                reviewBy = name,
                CreatedAt = DateTime.UtcNow
            };

            return _reviewRepository.Add(review);
        }

        public List<Review> GetReviewsByProduct(int productId)
        {
            return _reviewRepository.GetReviewsByProduct(productId);
        }
    }
}
