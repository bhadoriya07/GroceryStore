using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IReviewRepository
    {
        Review Add(Review review);
        List<Review> GetReviewsByProduct(int productId);
    }
}
