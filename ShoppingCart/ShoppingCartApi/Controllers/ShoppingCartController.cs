
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ShoppingCartModels.Carts;
using ShoppingCartBusiness.Services;
using ShoppingCartApi.Helpers;
using ShoppingCartApi.Controllers.Models;

namespace ShoppingCartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {

        private readonly ILogger<ShoppingCartController> _logger;
        private readonly ICartService _cartService;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        
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

        [HttpPost]
        [Route("AddItem")]
        public async Task<CartItem> AddItem([FromBody] CartAddItem addItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest();
                    return null;
                }

                return await _cartService.AddItem(IdentityHelper.GetUserId(User), addItem.ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError("ShoppingCartController.Get", ex);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [HttpPut]
        [Route("UpdateItem")]
        public async Task UpdateItem([FromBody] CartUpdateItem updateItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest();
                }

                await _cartService.UpdateItem(IdentityHelper.GetUserId(User), updateItem.ProductId, updateItem.Quantity);
                Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("ShoppingCartController.Get", ex);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        [HttpDelete]
        [Route("RemoveItem")]
        public async Task RemoveItem(int productId)
        {
            try
            {
                // Get UserId
                await _cartService.RemoveItem(IdentityHelper.GetUserId(User), productId);
                Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("ShoppingCartController.Get", ex);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}