using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApi.Controllers.Models
{
    public class CartAddItem
    {
        [Required]
        public int ProductId { get; set; }
    }
}
