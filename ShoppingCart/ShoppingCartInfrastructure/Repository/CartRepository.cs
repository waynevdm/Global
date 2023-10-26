using Dapper;
using Microsoft.Extensions.Configuration;
using ShoppingCartModels.Carts;
using System.Data.SqlClient;

namespace ShoppingCartInfrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly string connectionString;
        public CartRepository(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            connectionString = configuration.GetConnectionString("ShoppingCartConnectionString");
        }
        public async Task<Cart> CreateCart(int userId)
        {
            int cartId = -1;
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = @"INSERT INTO Cart (UserId) VALUES (@UserId);
                            SELECT CAST(SCOPE_IDENTITY() AS INT)";
                cartId = await connection.QuerySingleAsync<int>(sql, new { UserId = userId });
            }
            return new Cart { CartId = cartId, UserId = userId };
        }
        public async Task<Cart> GetCart(int userId)
        {
            Cart? cart;
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = @"SELECT * FROM Cart WHERE UserId = @UserId";
                cart = await connection.QuerySingleOrDefaultAsync<Cart>(sql, new { UserId = userId });

                if (cart != null)
                {
                    sql = @"SELECT * FROM CartItem WHERE CartId = @CartId";
                    var cartItems = await connection.QueryAsync<CartItem>(sql, new { CartId = cart.CartId });
                    foreach (var cartItem in cartItems)
                    {
                        cart.AddItem(cartItem);
                    }
                }
            }
            return cart;
        }
    }
}
