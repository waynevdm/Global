using System.ComponentModel.DataAnnotations;


namespace ShoppingCartModels.Carts
{
    public class CartItem
    {
        [Required]
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImagePath { get; set; }
        public decimal Price { get; set; }
        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }

        public decimal TotalPrice => Price * Quantity;
    }
}
