using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComProductCategoryToLayouts")]
    public class ProductCategoryToLayout : DomainEntity<Guid>
    {
        public ProductCategoryToLayout()
        {
        }

        public ProductCategoryToLayout(Guid productCategoryId, Guid storeId, Guid layoutId)
        {
            ProductCategoryId = productCategoryId;
            StoreId = storeId;
            LayoutId = layoutId;
        }

        [Required]
        public Guid ProductCategoryId { set; get; }

        [Required]
        public Guid StoreId { set; get; }

        [Required]
        public Guid LayoutId { set; get; }
    }
}