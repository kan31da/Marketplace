using Marketplace.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Models
{
    public class OrderProductViewModel
    {       
        public decimal TotalPrice { get; set; }       

        [Required]
        [StringLength(GlobalConstants.DeliveryAddress.MAX_LENTH, ErrorMessage = "The address must be no more that 200 characters")]
        public string DeliveryAddress { get; set; }
      
        public IEnumerable<CartProductsViewModel> Products { get; set; }
    }
}
