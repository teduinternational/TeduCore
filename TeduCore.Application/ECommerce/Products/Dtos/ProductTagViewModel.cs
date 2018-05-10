using System;
using TeduCore.Application.Dtos;

namespace TeduCore.Application.ECommerce.Products.Dtos
{
    public class ProductTagViewModel
    {
        public Guid productId { set; get; }

        public string TagId { set; get; }
    }
}