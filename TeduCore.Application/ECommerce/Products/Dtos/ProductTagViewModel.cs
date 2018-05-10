using TeduCore.Application.Dtos;

namespace TeduCore.Application.ECommerce.Products.Dtos
{
    public class ProductTagViewModel
    {
        public Guid productId { set; get; }

        public string TagId { set; get; }

        public virtual ProductViewModel Post { set; get; }

        public virtual TagViewModel Tag { set; get; }
    }
}