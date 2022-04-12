using Marketplace.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Models
{
    public class CartProductsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        [Required]
        [Range(GlobalConstants.CartProduct.FIRST_QUANTITY,
            GlobalConstants.CartProduct.QUANTITY_MAX,
            ErrorMessage = "Minimum Quantity is 1")]
        public int Quantity { get; set; }

        public string Image { get; set; }
    }
}
