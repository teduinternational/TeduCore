﻿namespace TeduCore.Application.ECommerce.Products.Dtos
{
    public class ProductImageViewModel
    {
        public Guid id { get; set; }

        public Guid productId { get; set; }

        public ProductViewModel Product { get; set; }

        public string Path { get; set; }

        public string Caption { get; set; }
    }
}