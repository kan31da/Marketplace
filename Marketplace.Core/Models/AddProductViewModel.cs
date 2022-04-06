using Marketplace.Core.Constants;
using Marketplace.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Models
{
    public class AddProductViewModel
    {
        [Required]
        [StringLength(ProductConstants.AddProduct.NAME_LENTGH,
            ErrorMessage = ErrorMessages.AddProduct.INVALID_PRODUCT_NAME_LENGTH)]
        public string Name { get; set; }
        //

        [Required]
        [Range(ProductConstants.AddProduct.MIN_VALUE,
            ProductConstants.AddProduct.MAX_VALUE,
            ErrorMessage = ErrorMessages.AddProduct.INVALID_PRODUCT_PRICE)]
        public string Price { get; set; }
        //

        [Required]
        [StringLength(ProductConstants.AddProduct.NAME_LENTGH,
            ErrorMessage = ErrorMessages.AddProduct.INVALID_PRODUCT_DESCRIPTION_LENTGH)]
        public string Description { get; set; }
        //

        [Required]
        [Range(ProductConstants.AddProduct.QUANTITY_MIN,
            ProductConstants.AddProduct.QUANTITY_MAX,
            ErrorMessage = ErrorMessages.AddProduct.INVALID_PRODUCT_QUANTITY_LENTGH)]
        public string Quantity { get; set; }
        //

        [Required]
        [StringLength(ProductConstants.AddProduct.IMAGE_PATH_LENTGH,
            ErrorMessage = ErrorMessages.AddProduct.INVALID_PRODUCT_NAME_LENGTH)]
        public string FirstImagePath { get; set; }
    }
}
