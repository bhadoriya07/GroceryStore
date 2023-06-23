using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IReviewService
    {
        Review PostReview(int productId, string reviewContent, string name);
        List<Review> GetReviewsByProduct(int productId);
    }
}
