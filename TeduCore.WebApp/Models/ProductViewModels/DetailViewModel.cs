﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCore.Services.ViewModels;

namespace TeduCore.WebApp.Models.ProductViewModels
{
    public class DetailViewModel
    {
        public ProductViewModel Product { get; set; }

        public List<ProductViewModel> RelatedProducts { get; set; }

        public ProductCategoryViewModel Category { get; set; }

        public List<ProductImageViewModel> ProductImages { set; get; }

        public List<ProductViewModel> UpsellProducts { get; set; }

        public List<ProductViewModel> LastestProducts { get; set; }


    }
}
