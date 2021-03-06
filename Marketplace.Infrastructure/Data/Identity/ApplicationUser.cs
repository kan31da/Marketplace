using Marketplace.Infrastructure.Data.Models;
using Marketplace.Infrastructure.DataConstants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(ModelConstants.FIRSTNAME_LENTGH)]
        public string? FirstName { get; set; }

        [StringLength(ModelConstants.LASTNAME_LENTGH)]
        public string? LastName { get; set; }

        [StringLength(ModelConstants.PHONE_LENTGH)]
        public string? PhoneNumber { get; set; }

        public bool Is_Deleted { get; set; } = false;

        [Required]
        public Cart Cart { get; set; } = new Cart();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
