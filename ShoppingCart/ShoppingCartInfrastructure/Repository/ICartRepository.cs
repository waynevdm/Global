using ShoppingCartModels.Carts;
using ShoppingCartModels.Products;


namespace ShoppingCartInfrastructure.Repository
{
    public interface ICartRepository
    {
        public Task<Cart> CreateCart(int userId);
        public Task<Cart> GetCart(int userId);
        public Task<CartItem> AddItem(int userId, Product prod);
        public Task UpdateItem(int userId, int productId, int quantity);
        public Task RemoveItem(int userId, int productId);
    }
}
