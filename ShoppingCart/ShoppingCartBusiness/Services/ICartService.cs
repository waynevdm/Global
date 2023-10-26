using ShoppingCartModels.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartBusiness.Services
{
    public interface ICartService
    {
        public Task<Cart> GetCart(int userId);
    }
}
