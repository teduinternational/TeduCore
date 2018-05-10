using System;
using TeduCore.Application.ECommerce.Products.Dtos;

namespace TeduCore.Application.ECommerce.Bills.Dtos
{
    public class BillDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid BillId { set; get; }

        public Guid ProductId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        public BillViewModel Bill { set; get; }

        public ProductViewModel Product { set; get; }

    }
}