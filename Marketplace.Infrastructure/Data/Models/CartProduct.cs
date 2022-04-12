using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data.Models
{
    public class CartProduct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        //

        public Guid ProductId { get; set; } 
        //

        [Required]
        [StringLength(ModelConstants.NAME_LENTGH)]
        public string Name { get; set; }
        //

        [Required]
        [Column(TypeName = ModelConstants.PRODUCT_DECIMAL_PRECISION)]
        public decimal Price { get; set; }
        //

        [Required]
        [Range(ModelConstants.QUANTITY_MIN, ModelConstants.QUANTITY_MAX)]
        public int Quantity { get; set; }
        //

        [Required]
        [StringLength(ModelConstants.IMAGE_PATH_LENTGH)]
        public string ImagePath { get; set; }
    }
}
