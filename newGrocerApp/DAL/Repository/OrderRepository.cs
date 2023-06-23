using DAL.Data;
using DAL.Interface;
using DomainModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly GroceryAppDBContext _context;

        public OrderRepository(GroceryAppDBContext context)
        {
            _context = context;
        }

        public async Task PlaceOrder(string userId, List<Product> products)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now
            };

            // Add the order to the context
            _context.Orders.Add(order);

            // Create a separate list to hold the products temporarily
            var orderedProducts = new List<Product>(products);

            // Associate the products with the order
            foreach (var product in orderedProducts)
            {
                order.Products.Add(product);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetOrderedProducts(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .SelectMany(o => o.Products)
                .ToListAsync();
        }

    }
}
