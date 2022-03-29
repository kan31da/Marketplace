using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [Required]
        [Column(TypeName = ModelConstants.ORDER_DECIMAL_PRECISION)]
        public decimal OrderPrice { get; set; }

        //[ForeignKey("Id")]
        //public virtual ApplicationUser User { get; set; }
    }
}