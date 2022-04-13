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
        [StringLength(ModelConstants.ORDERSTATUS_LENTGH)]
        public string OrderStatus { get; set; } 

        public ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();

        [Required]
        [Column(TypeName = ModelConstants.ORDER_DECIMAL_PRECISION)]
        public decimal OrderPrice { get; set; }

        [Required]
        [StringLength(ModelConstants.DELIVERY_ADDRESS_LENTGH)]
        public string DeliveryAddress { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public DateTime? DeliveryDate { get; set; }

        public Shipper? Shipper { get; set; }
    }
}