using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetAllProducts();
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Product product);
        Task<List<Product>> GetProductsByCategory(string category);
        Task<List<Product>> SearchProducts(string keyword);
        Task UpdateUser(ApplicationUser user);
        Task AddProductToCart(string userId, int productId);
        Task<List<Product>> GetProductsInCart(string userId);
        Task RemoveProductFromCart(string userId, int productId);
        Task ClearCart(string userId);

    }


}
