using BLL.Interface;
using DAL.Data;
using DAL.Interface;
using DomainModelLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GroceryAppDBContext _context;

        public ProductService(IProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            // Perform any additional business logic or validations here

            var createdProduct = await _productRepository.CreateProduct(product);
            return createdProduct;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return products;
        }

        public async Task<Product> UpdateProduct(int id, ProductDto productDto)
        {
            var existingProduct = await _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Category = productDto.Category;
            existingProduct.Quantity = productDto.AvailableQuantity;
            existingProduct.Price = productDto.Price;
            existingProduct.Discount = productDto.Discount;
            existingProduct.Specification = productDto.Specification;

            var updatedProduct = await _productRepository.UpdateProduct(existingProduct);
            return updatedProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var existingProduct = await _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return false;
            }

            var isDeleted = await _productRepository.DeleteProduct(existingProduct);
            return isDeleted;
        }

        public async Task<List<Product>> GetProductsByCategory(string category)
        {
            return await _productRepository.GetProductsByCategory(category);
        }

        public async Task<List<Product>> SearchProducts(string keyword)
        {
            return await _productRepository.SearchProducts(keyword);
        }

        public async Task AddToCart(string userId, int productId)
        {
            await _productRepository.AddProductToCart(userId, productId);
        }

        public async Task RemoveFromCart(string userId, int productId)
        {
            await _productRepository.RemoveProductFromCart(userId, productId);
        }

        public async Task<List<Product>> GetCartProducts(string userId)
        {
            var cartProducts = await _productRepository.GetProductsInCart(userId);
            return cartProducts;
        }

    }


}
