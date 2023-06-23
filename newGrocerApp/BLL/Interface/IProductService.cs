using DomainModelLayer;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetAllProducts();
        Task<Product> UpdateProduct(int id, ProductDto productDto);
        Task<bool> DeleteProduct(int id);
        Task<List<Product>> GetProductsByCategory(string category);
        Task<List<Product>> SearchProducts(string keyword);
        Task AddToCart(string userId, int productId);
        Task<List<Product>> GetCartProducts(string userId);
        Task RemoveFromCart(string userId, int productId);

    }


}
