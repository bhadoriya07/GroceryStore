using BLL.Interface;
using DAL.Migrations;
using DomainModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Shared.DTOs;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace newGrocerApp.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Category = productDto.Category,
                    Quantity = productDto.AvailableQuantity,
                    Price = productDto.Price,
                    Discount = productDto.Discount,
                    Specification = productDto.Specification
                };

                var createdProduct = await _productService.CreateProduct(product);

                if (productDto.Image != null && productDto.Image.Length > 0)
                {
                    var fileName = $"{createdProduct.Id}.jpg";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.Image.CopyToAsync(stream);
                    }

                    createdProduct.ImageUrl = GenerateImageUrl(createdProduct.Id);// Update the image URL
                }

                return Ok(createdProduct);
            }

            return BadRequest(ModelState);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {

                var updatedProduct = await _productService.UpdateProduct(id, productDto);

                if (productDto.Image != null && productDto.Image.Length > 0)
                {
                    // Delete the existing image
                    var existingImagePath = GetImagePath(id);
                    if (System.IO.File.Exists(existingImagePath))
                    {
                        System.IO.File.Delete(existingImagePath);
                    }

                    // Save the new image
                    var newImageFileName = $"{id}.jpg";
                    var newImageFilePath = Path.Combine(GetImageDirectory(), newImageFileName);

                    using (var stream = new FileStream(newImageFilePath, FileMode.Create))
                    {
                        await productDto.Image.CopyToAsync(stream);
                    }

                    updatedProduct.ImageUrl = GenerateImageUrl(updatedProduct.Id); // Update the image URL
                }


                if (updatedProduct == null)
                {
                    return NotFound();
                }

                return Ok(updatedProduct);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isDeleted = await _productService.DeleteProduct(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory(string category)
        {
            var products = await _productService.GetProductsByCategory(category);
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProducts([FromQuery] string keyword)
        {
            var products = await _productService.SearchProducts(keyword);
            return Ok(products);
        }


        [HttpPost("cart")]
        public async Task<ActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            string userId = addToCartDto.UserId;
            await _productService.AddToCart(userId, addToCartDto.ProductId);
            return Ok();
        }
        [HttpGet("cartProducts")]
        public async Task<ActionResult<List<Product>>> GetCartProducts(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(); // User not found
            }

            var cartProducts = await _productService.GetCartProducts(user.Id);
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var serializedProducts = JsonSerializer.Serialize(cartProducts, jsonOptions);

            return Content(serializedProducts, "application/json");
        }

        [HttpDelete("remove/{userId}/{productId}")]
        public async Task<ActionResult> RemoveFromCart(string userId, int productId)
        {
            await _productService.RemoveFromCart(userId, productId);
            return Ok();
        }

        private string GenerateImageUrl(int productId)
        {
            var imageUrl = $"http://localhost:15057/api/products/images/{productId}.jpg";
            return imageUrl;
        }

        private string GetImageDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Images");
        }
        private string GetImagePath(int productId)
        {
            var fileName = $"{productId}.jpg";
            return Path.Combine(GetImageDirectory(), fileName);
        }
    }

}
