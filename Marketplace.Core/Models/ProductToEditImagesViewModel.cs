using Marketplace.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Models
{
    public class ProductToEditImagesViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(ModelConstants.IMAGE_PATH_LENTGH)]
        public string Name { get; set; }
        public IEnumerable<ImageViewModel> Images { get; set; }
    }
}
