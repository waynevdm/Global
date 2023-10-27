using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApi.Controllers.Models
{
    public class CartUpdateItem
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

    }
}
