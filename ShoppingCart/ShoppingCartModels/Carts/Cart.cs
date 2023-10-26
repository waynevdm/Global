

namespace ShoppingCartModels.Carts
{
    public class Cart
    {
        public int CartId { get; set; }
        // This will be set by the identity context
        public int UserId { get; set; }
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
