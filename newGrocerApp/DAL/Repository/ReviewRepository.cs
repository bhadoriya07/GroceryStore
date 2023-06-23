using DAL.Data;
using DAL.Interface;
using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly GroceryAppDBContext _dbContext;

        public ReviewRepository(GroceryAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Review Add(Review review)
        {
            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();
            return review;
        }

        public List<Review> GetReviewsByProduct(int productId)
        {
            return _dbContext.Reviews
                .Where(r => r.ProductId == productId)
                .ToList();
        }
    }
}
