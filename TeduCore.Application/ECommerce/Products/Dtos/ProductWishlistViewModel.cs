﻿using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Application.Systems.Users.Dtos;

namespace TeduCore.Application.ECommerce.Products.Dtos
{
    public class ProductWishlistViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string UserId { get; set; }

        public  AppUserViewModel AppUser { get; set; }

        public  ProductViewModel Product { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
