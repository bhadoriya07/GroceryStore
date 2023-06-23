using DAL.Data;
using DAL.Interface;
using DomainModelLayer;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly GroceryAppDBContext _context;

        public ProductRepository(GroceryAppDBContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<List<Product>> GetProductsByCategory(string category)
        {
            return await _context.Products.Where(p => p.Category == category).ToListAsync();
        }

        public async Task<List<Product>> SearchProducts(string keyword)
        {
            return await _context.Products.Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword)).ToListAsync();
        }

        public async Task AddProductToCart(string userId, int productId)
        {
            var user = await _context.Users
                .Include(u => u.Cart)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var product = await _context.Products.FindAsync(productId);

            if (user == null || product == null)
            {
                throw new NotFoundException("User or Product not found");
            }

            if (user.Cart == null)
            {
                user.Cart = new Cart { UserId = userId };
            }

            user.Cart.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductFromCart(string userId, int productId)
        {
            var user = await _context.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Cart == null)
            {
                throw new NotFoundException("User or Cart not found");
            }

            var product = user.Cart.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                throw new NotFoundException("Product not found in cart");
            }

            user.Cart.Products.Remove(product);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateUser(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsInCart(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Cart == null)
            {
                return new List<Product>(); // Return an empty list if user or cart not found
            }

            return user.Cart.Products;
        }

        public async Task ClearCart(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null && user.Cart != null)
            {
                user.Cart.Products.Clear();
                await _context.SaveChangesAsync();
            }
        }

    }

}
