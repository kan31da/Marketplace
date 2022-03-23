using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Infrastructure.Data
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(ModelConstants.LABEL_LENTGH)]
        public string Label { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
