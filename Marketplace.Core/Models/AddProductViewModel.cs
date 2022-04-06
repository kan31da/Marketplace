namespace Marketplace.Core.Models
{
    public class AddProductViewModel
    {
        public string CategoryLabel { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }      
        public string FirstImagePath { get; set; }      
        public string SupplierCompanyName { get; set; }      
        public string SupplierPhone { get; set; }
        public string SupplierAddress { get; set; }
    }
}
