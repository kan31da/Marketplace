using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data.Models
{
    public class OrderStatus
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [Required]
        [StringLength(ModelConstants.ORDERSTATUS_LENTGH)]
        public string Status { get; set; }
    }
}
