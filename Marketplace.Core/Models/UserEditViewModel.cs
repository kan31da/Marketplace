using Marketplace.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        [Required]
        [RegularExpression(GlobalConstants.UserEdit.REGULAREXPRESSION_IS_DELETED,
            ErrorMessage = ErrorMessages.UserEdit.REGULAREXPRESSION_ERROR_MESSAGES)]
        public string Is_Deleted { get; set; }
    }
}
