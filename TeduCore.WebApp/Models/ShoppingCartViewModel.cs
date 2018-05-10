using TeduCore.Application.ECommerce.Products.Dtos;

namespace TeduCore.WebApp.Models
{
    public class ShoppingCartViewModel
    {
        public ProductViewModel Product { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }
    }
}