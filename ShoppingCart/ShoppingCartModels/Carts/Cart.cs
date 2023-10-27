

namespace ShoppingCartModels.Carts
{
    public class Cart
    {
        public int CartId { get; set; }
        // This will be set by the identity context
        public int UserId { get; set; }
        public IList<CartItem> Items = new List<CartItem>();

        public void UpdateItemQuantity(CartItem item, int quantity)
        {
            item.Quantity = quantity;
        }

        public decimal GetTotalPrice()
        {
            return Items.Sum(item => item.TotalPrice);
        }
    }
}
