namespace Marketplace.Core.Models
{
    public class ProductToEditViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Quantity { get; set; }
        public IEnumerable<ImageViewModel> Images { get; set; }
    }
}
