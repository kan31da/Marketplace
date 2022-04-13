namespace Marketplace.Core.Models
{
    public class OrderHistoryViewModel
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderPrice { get; set; }
        public string OrderDate { get; set; } 
        public string DeliveryDate { get; set; }
    }
}
