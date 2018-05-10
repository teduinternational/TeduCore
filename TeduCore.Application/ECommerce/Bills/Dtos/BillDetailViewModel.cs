using TeduCore.Application.ECommerce.Products.Dtos;

namespace TeduCore.Application.ECommerce.Bills.Dtos
{
    public class BillDetailViewModel
    {
        public Guid id { get; set; }
        public Guid billId { set; get; }

        public Guid productId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        public BillViewModel Bill { set; get; }

        public ProductViewModel Product { set; get; }

    }
}