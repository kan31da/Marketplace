using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data.Models
{
    public class Shipper
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required]
        [StringLength(ModelConstants.SUPPLIER_NAME_LENTGH)]
        public string Name { get; set; }


        [Required]
        [StringLength(ModelConstants.SUPPLIER_PHONE_LENTG)]
        public string Phone { get; set; }


        [Required]
        public DateTime DeliveryDate { get; set; }


        public ICollection<Order> Order { get; set; } = new List<Order>();
    }
}
