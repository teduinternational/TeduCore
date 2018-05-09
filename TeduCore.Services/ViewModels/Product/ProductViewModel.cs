﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeduCore.Infrastructure.Enums;

namespace TeduCore.Services.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; }

        public string Code { set; get; }

        [Required]
        public int CategoryId { set; get; }

        [MaxLength(256)]
        public string ThumbnailImage { set; get; }

        public decimal Price { set; get; }

        public decimal OriginalPrice { set; get; }

        public decimal? PromotionPrice { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }

        public string Tags { set; get; }

        public int Quantity { set; get; }

        [StringLength(50)]
        public string Unit { get; set; }

        public ProductCategoryViewModel ProductCategory { set; get; }

        public ICollection<ProductTagViewModel> ProductTags { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }
        public string SeoPageTitle { set; get; }
        public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }
    }
}