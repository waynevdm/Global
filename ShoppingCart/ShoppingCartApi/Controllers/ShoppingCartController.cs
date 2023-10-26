
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ShoppingCartModels.Carts;
using ShoppingCartBusiness.Services;
using ShoppingCartApi.Helpers;

namespace ShoppingCartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {

        private readonly ILogger<ShoppingCartController> _logger;
        private readonly ICartService _cartService;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet(Name = "GetShoppingCart")]
        public async Task<Cart> Get()
        {
            try
            {
                // Get UserId
                return await _cartService.GetCart(IdentityHelper.GetUserId(User));
            }
            catch (Exception ex) 
            {
                _logger.LogError("ShoppingCartController.Get", ex);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }
        }
    }
}