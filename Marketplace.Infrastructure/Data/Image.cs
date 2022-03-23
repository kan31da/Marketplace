using System.ComponentModel.DataAnnotations;

namespace Marketplace.Infrastructure.Data
{
    public class Image
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ImagePath { get; set; }
    }
}
