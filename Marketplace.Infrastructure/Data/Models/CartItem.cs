using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data.Models
{
    public class CartItem
    {        
        public Guid CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }

        
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
