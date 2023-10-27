using ShoppingCartModels.Carts;

namespace ShoppingCartBusiness.Services
{
    public interface ICartService
    {
        public Task<Cart> GetCart(int userId);
        public Task<CartItem> AddItem(int userId, int productId);
        public Task UpdateItem(int userId, int productId, int quantity);
        public Task RemoveItem(int userId, int productId);
    }
}
