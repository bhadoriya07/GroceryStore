using BLL.Interface;
using DAL.Interface;
using DAL.Repository;
using DomainModelLayer;
using Microsoft.AspNetCore.Identity;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, UserManager<ApplicationUser> userManager, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        public async Task PlaceOrder(string userId)
        {
            // Retrieve the products in the user's cart
            List<Product> cartProducts = await _productRepository.GetProductsInCart(userId);

            if (cartProducts.Count == 0)
            {
                throw new NotFoundException("No products found in the cart.");
            }

            // Place the order
            await _orderRepository.PlaceOrder(userId, cartProducts);

            // Clear the cart
            await _productRepository.ClearCart(userId);
        }

        public async Task<List<Product>> GetOrderedProducts(string userId)
        {
            return await _orderRepository.GetOrderedProducts(userId);
        }
    }
}
