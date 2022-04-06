using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(ModelConstants.NAME_LENTGH)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = ModelConstants.PRODUCT_DECIMAL_PRECISION)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(ModelConstants.DESCRIPTION_LENTGH)]
        public string Description { get; set; }        

        [Required]
        [Range(ModelConstants.QUANTITY_MIN, ModelConstants.QUANTITY_MAX)]
        public int Quantity { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
