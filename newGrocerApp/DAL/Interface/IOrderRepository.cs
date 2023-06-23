using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IOrderRepository
    {
        Task PlaceOrder(string userId, List<Product> products);
        Task<List<Product>> GetOrderedProducts(string userId);
    }
}
