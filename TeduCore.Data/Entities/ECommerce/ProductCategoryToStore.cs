using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComProductCategoryToStores")]
    public class ProductCategoryToStore : DomainEntity<Guid>
    {
        public ProductCategoryToStore()
        {
        }

        public ProductCategoryToStore(Guid productCategoryId, Guid storeId)
        {
            ProductCategoryId = productCategoryId;
            StoreId = storeId;
        }

        [Required]
        public Guid ProductCategoryId { set; get; }

        [Required]
        public Guid StoreId { set; get; }
    }
}