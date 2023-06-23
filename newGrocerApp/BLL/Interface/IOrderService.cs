using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IOrderService
    {
        Task PlaceOrder(string userId);
        Task<List<Product>> GetOrderedProducts(string userId);
    }
}
