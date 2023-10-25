using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartBusiness.Models
{
    public class Cart
    {
        private List<CartItem> _items = new List<CartItem>();

        public void AddItem(CartItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(CartItem item)
        {
            _items.Remove(item);
        }

        public void UpdateItemQuantity(CartItem item, int quantity)
        {
            item.Quantity = quantity;
        }

        public IEnumerable<CartItem> GetItems()
        {
            return _items;
        }

        public decimal GetTotalPrice()
        {
            return _items.Sum(item => item.TotalPrice);
        }
    }
}
