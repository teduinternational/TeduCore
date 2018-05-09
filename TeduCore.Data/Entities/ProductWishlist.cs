using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("ProductWishlists")]
    public class ProductWishlist : DomainEntity<int>, IDateTracking
    {
        public ProductWishlist()
        {
        }

        public ProductWishlist(int id, string userId, int productId)
        {
            Id = id;
            UserId = userId;
            ProductId = productId;
        }

        public int ProductId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}