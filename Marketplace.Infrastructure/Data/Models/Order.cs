using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public OrderStatus OrderStatus { get; set; } = new OrderStatus();

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [Required]
        [Column(TypeName = ModelConstants.ORDER_DECIMAL_PRECISION)]
        public decimal OrderPrice { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public DateTime? DeliveryDate { get; set; }

        public Shipper Shipper { get; set; }
    }
}