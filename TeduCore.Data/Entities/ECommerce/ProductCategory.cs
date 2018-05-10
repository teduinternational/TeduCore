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
    [Table("ProductCategories")]
    public class ProductCategory : DomainEntity<Guid>, IHasSeoMetaData,
        ISwitchable, ISortable, IDateTracking
    {
        public ProductCategory()
        {
        }

        public ProductCategory(Guid id, string name, string code, string description, Guid? parentId, int? homeOrder, string image, bool? homeFlag,
            int sortOrder, Status status, string seoPageTitle, string seoAlias, string seoKeywords, string seoDescriptions)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
            ParentId = parentId;
            HomeOrder = homeOrder;
            Image = image;
            HomeFlag = homeFlag;
            SortOrder = sortOrder;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescriptions;
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(50)]
        public string Code { get; set; }

        [DefaultValue(0)]
        public int CurrentIdentity { get; set; }

        [MaxLength(500)]
        public string Description { set; get; }

        public Guid? ParentId { set; get; }

        public int? HomeOrder { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        public bool? HomeFlag { set; get; }

        public DateTime DateCreated { set; get; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }

        [MaxLength(256)]
        public string SeoPageTitle { set; get; }

        [MaxLength(256)]
        public string SeoAlias { set; get; }

        [MaxLength(256)]
        public string SeoKeywords { set; get; }

        [MaxLength(256)]
        public string SeoDescription { set; get; }
        public DateTime? DateDeleted { set; get; }
        public DateTime? DateModified { set; get; }
    }
}