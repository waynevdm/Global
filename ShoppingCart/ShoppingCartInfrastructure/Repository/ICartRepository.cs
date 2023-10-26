using ShoppingCartModels.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartInfrastructure.Repository
{
    public interface ICartRepository
    {
        public Task<Cart> CreateCart(int userId);
        public Task<Cart> GetCart(int userId);
    }
}
