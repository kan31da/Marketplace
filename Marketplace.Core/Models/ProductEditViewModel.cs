using Marketplace.Core.Constants;
using Marketplace.Core.Utilities;
using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Core.Models
{
    public class ProductEditViewModel
    {
        public string Id { get; set; }
        //

        [Required]
        [StringLength(ProductConstants.AddProduct.NAME_LENTGH,
             ErrorMessage = ErrorMessages.AddProduct.INVALID_PRODUCT_NAME_LENGTH)]
        public string Name { get; set; }
        //

        [Required]
        [Range(ProductConstants.AddProduct.MIN_VALUE,
           ProductConstants.AddProduct.MAX_VALUE,
           ErrorMessage = ErrorMessages.AddProduct.INVALID_PRODUCT_PRICE)]
        public decimal Price { get; set; }
        //

        [Required]
        [StringLength(ModelConstants.DESCRIPTION_LENTGH)]
        public string Description { get; set; }
        //

        [Required]
        [Range(ModelConstants.QUANTITY_MIN, ModelConstants.QUANTITY_MAX)]
        public int Quantity { get; set; }
    }
}
