using ShoppingCartInfrastructure.Repository;
using ShoppingCartModels.Carts;
using ShoppingCartModels.Products;

namespace ShoppingCartBusiness.Services
{
    public class CartService : ICartService
    {
        // This should be stored in a database and loaded and cached for performance.
        private List<Product> products = new List<Product> {
            new Product { Id = 1, Name = "Watch", ImagePath = "https://images.unsplash.com/photo-1523275335684-37898b6baf30", Price = 30.50M },
            new Product { Id = 2, Name = "Camera", ImagePath = "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f", Price = 53.67M },
            new Product { Id = 3, Name = "Headphones", ImagePath = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e", Price = 15.35M },
        };

        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Cart> GetCart(int userId)
        {
            var cart = await _cartRepository.GetCart(userId);
            if (cart == null)
            {
                cart = await _cartRepository.CreateCart(userId);
            }

            return cart;
        }

        public async Task<CartItem> AddItem(int userId, int productId)
        {
            var product = ValidateProductId(productId);

            return await _cartRepository.AddItem(userId, product);
        }
        public async Task UpdateItem(int userId, int productId, int quantity)
        {
            ValidateProductId(productId);
            await _cartRepository.UpdateItem(userId, productId, quantity);
        }
        public async Task RemoveItem(int userId, int productId)
        {
            ValidateProductId(productId);

            await _cartRepository.RemoveItem(userId, productId);
        }

        private Product ValidateProductId(int productId)
        {
            Product prod = products.FirstOrDefault(x => x.Id == productId);
            if (prod == null)
                throw new ArgumentException("The product id is invalid");
            return prod;
        }
    }
}