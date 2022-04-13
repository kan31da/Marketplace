namespace Marketplace.Core.Models
{
    public class OrdersViewModel
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal OrderPrice { get; set; }
        public string OrderDate { get; set; }
    }
}
