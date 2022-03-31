using Marketplace.Infrastructure.Data.Models;
using Marketplace.Infrastructure.DataConstants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(ModelConstants.FIRSTNAME_LENTGH)]
        [Required(ErrorMessage = "Please enter FirstName")]
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }
        //

        [StringLength(ModelConstants.LASTNAME_LENTGH)]
        [Required(ErrorMessage = "Please enter LastName")]
        [Display(Name = "LastName")]
        public string? LastName { get; set; }
        //

        public Nullable<bool> Is_Deleted { get; set; }
        //

        [Required]
        public Cart Cart { get; set; } = new Cart();
        //

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
