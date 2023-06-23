using BLL.Interface;
using BLL.Services;
using DomainModelLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace newGrocerApp.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string userId)
        {

            await _orderService.PlaceOrder(userId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderedProducts(string userId)
        {
            try
            {
                var orderedProducts = await _orderService.GetOrderedProducts(userId);
                return Ok(orderedProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
