using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Core.Models
{
    public class ProductViewModel
    {

        [Required]
        [StringLength(ModelConstants.NAME_LENTGH)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [StringLength(ModelConstants.DESCRIPTION_LENTGH)]
        public string Description { get; set; }

        [Required]
        [Range(ModelConstants.QUANTITY_MIN, ModelConstants.QUANTITY_MAX)]
        public int Quantity { get; set; }
       
    }
}
