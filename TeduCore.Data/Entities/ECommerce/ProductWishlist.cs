using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("ProductWishlists")]
    public class ProductWishlist : DomainEntity<Guid>, IDateTracking
    {
        public ProductWishlist()
        {
        }

        public ProductWishlist(Guid id, Guid userId, Guid productId)
        {
            Id = id;
            UserId = userId;
            ProductId = productId;
        }

        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }
    }
}