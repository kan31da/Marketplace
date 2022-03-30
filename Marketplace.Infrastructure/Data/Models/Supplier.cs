using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data.Models
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(ModelConstants.SUPPLIER_NAME_LENTGH)]
        public string CompanyName { get; set; }


        [Required]
        [StringLength(ModelConstants.SUPPLIER_PHONE_LENTG)]
        public string Phone { get; set; }


        [Required]
        [StringLength(ModelConstants.SUPPLIER_ADDRESS_LENTGH)]
        public string Address { get; set; }


        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

    }
}
