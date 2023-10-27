using Dapper;
using Microsoft.Extensions.Configuration;
using ShoppingCartModels.Carts;
using ShoppingCartModels.Products;
using System.Data.SqlClient;
using System.Diagnostics;

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
                    cart.Items = (await connection.QueryAsync<CartItem>(sql, new { CartId = cart.CartId })).ToList();
                }
            }
            return cart;
        }

        public async Task<int> GetLastCartIdForUserId(int userId)
        {
            int cartId;
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = @"SELECT MAX(CartId) FROM Cart WHERE UserId = @UserId";
                cartId = await connection.QuerySingleOrDefaultAsync<int>(sql, new { UserId = userId });
            }
            return cartId;
        }

        public async Task<CartItem> AddItem(int userId, Product prod)
        {
            int intialQuantity = 1;
            int cartId = await GetLastCartIdForUserId(userId);
            var insertItem = new 
            {
                CartId = cartId,
                ProductId = prod.Id,
                ProductName = prod.Name,
                ProductImagePath = prod.ImagePath,
                Price = prod.Price,
                Quantity = intialQuantity
            };

            var sql = @"INSERT INTO CartItem (CartId, ProductId, ProductName, ProductImagePath, Price, Quantity) 
                                        VALUES (@CartId, @ProductId, @ProductName, @ProductImagePath, @Price, @Quantity);";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, insertItem);
            }

            return new CartItem
            {
                ProductId = prod.Id,
                ProductName = prod.Name,
                ProductImagePath = prod.ImagePath,
                Price = prod.Price,
                Quantity = intialQuantity
            };
        }

        public async Task UpdateItem(int userId, int productId, int quantity)
        {
            int cartId = await GetLastCartIdForUserId(userId);
            var updateItem = new
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity
            };

            var sql = @"UPDATE CartItem 
                            SET Quantity = @Quantity
                            WHERE CartId = @CartId 
                            AND ProductId = @ProductId;";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, updateItem);
            }
        }
        public async Task RemoveItem(int userId, int productId)
        {
            int cartId = await GetLastCartIdForUserId(userId);
            var deleteItem = new
            {
                CartId = cartId,
                ProductId = productId
            };

            var sql = @"DELETE FROM CartItem 
                            WHERE CartId = @CartId 
                            AND ProductId = @ProductId;";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, deleteItem);
            }
        }

    }
}
