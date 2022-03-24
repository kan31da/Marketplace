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
    }
}
