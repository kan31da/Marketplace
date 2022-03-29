using Marketplace.Infrastructure.DataConstants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(ModelConstants.FIRSTNAME_LENTGH)]
        public string? FirstName { get; set; }

        [StringLength(ModelConstants.LASTNAME_LENTGH)]
        public string? LastName { get; set; }
        
        [Required]
        public Cart Cart { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
