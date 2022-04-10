﻿namespace Marketplace.Core.Models
{
    public class ProductToEditViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<ImageViewModel> Images { get; set; }
    }
}