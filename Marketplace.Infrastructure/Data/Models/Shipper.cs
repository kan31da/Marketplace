using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Infrastructure.Data.Models
{
    public class Shipper
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }


        [Required]
        [StringLength(ModelConstants.SUPPLIER_NAME_LENTGH)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(ModelConstants.SUPPLIER_NAME_LENTGH)]
        public string LastName { get; set; }

        [Required]
        [StringLength(ModelConstants.SUPPLIER_PHONE_LENTG)]
        public string Phone { get; set; }  

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
