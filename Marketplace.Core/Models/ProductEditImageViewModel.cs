namespace Marketplace.Core.Models
{
    public class ProductEditImageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ImageViewModel> Images { get; set; }
    }
}
