using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Infrastructure.Data.Models
{
    public class Image
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(ModelConstants.IMAGE_PATH_LENTGH)]
        public string ImagePath { get; set; }
    }
}
