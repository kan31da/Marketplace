using Marketplace.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }


        [Required]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Phone Number Required!")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{2,9}$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }


        [Required]
        [RegularExpression(GlobalConstants.UserEdit.REGULAREXPRESSION_IS_DELETED,
            ErrorMessage = ErrorMessages.UserEdit.REGULAREXPRESSION_ERROR_MESSAGES)]
        public string Is_Deleted { get; set; }
    }
}
