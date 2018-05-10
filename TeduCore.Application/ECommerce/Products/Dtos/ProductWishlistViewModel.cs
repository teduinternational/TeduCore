using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Application.Systems.Users.Dtos;

namespace TeduCore.Application.ECommerce.Products.Dtos
{
    public class ProductWishlistViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
