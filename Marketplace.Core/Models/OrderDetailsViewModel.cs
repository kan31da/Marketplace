namespace Marketplace.Core.Models
{
    public class OrderDetailsViewModel
    {
        public string OrderId { get; set; }
        public string ShipperPhone { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public string ShipperName { get; set; }
        public decimal OrderPrice { get; set; }

        public IEnumerable<CartProductsViewModel> Products { get; set; }
    }
}
