﻿namespace Marketplace.Core.Models
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public IEnumerable<ImageViewModel> Images { get; set; }
    }
}
