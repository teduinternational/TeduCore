using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("Products")]
    public class Product : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Product()
        {
        }

        public Product(int id, string name, string code, int categoryId, string thumbnailImage,
             decimal price, decimal originalPrice, decimal? promotionPrice,
             string description, string content, bool? homeFlag, bool? hotFlag,
             string tags, int quantity, string unit, Status status, string seoPageTitle,
             string seoAlias, string seoMetaKeyword,
             string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Code = code;
            CategoryId = categoryId;
            ThumbnailImage = thumbnailImage;
            Price = price;
            OriginalPrice = originalPrice;
            PromotionPrice = promotionPrice;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Tags = tags;
            Quantity = quantity;
            Unit = unit;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(50)]
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

        [DefaultValue(0)]
        public int Quantity { set; get; }

        [StringLength(50)]
        public string Unit { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { set; get; }

        public virtual ICollection<ProductTag> ProductTags { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }

        [MaxLength(256)]
        public string SeoPageTitle { set; get; }

        [MaxLength(256)]
        public string SeoAlias { set; get; }

        [MaxLength(256)]
        public string SeoKeywords { set; get; }

        [MaxLength(256)]
        public string SeoDescription { set; get; }
    }
}