using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("Pages")]
    public class Page : DomainEntity<Guid>, ISwitchable
        ,IHasUniqueCode,IHasSeoMetaData
    {
        public Page()
        {
        }

        public Page(Guid id, string name, string uniqueCode,
            string content, Status status)
        {
            Id = id;
            Name = name;
            UniqueCode = uniqueCode;
            Content = content;
            Status = status;
        }


        [Required]
        [MaxLength(256)]
        public string Name { set; get; }


        public string Content { set; get; }
        public Status Status { set; get; }

        [Required]
        [MaxLength(256)]
        public string UniqueCode { set; get; }
        public string SeoPageTitle { set; get; }
        public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }
    }
}