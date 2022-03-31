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
        [RegularExpression($"^([Tt][Rr][Uu][Ee]|[Ff][Aa][Ll][Ss][Ee])$", ErrorMessage = "Is Deleted has only two options, True or False")]
        public string Is_Deleted { get; set; }
    }
}
