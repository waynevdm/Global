using ShoppingCartBusiness.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {

        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(ILogger<ShoppingCartController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetShoppingCart")]
        public async Task<Cart> Get()
        {
            return await Task.FromResult<Cart>(new Cart());
        }
    }
}