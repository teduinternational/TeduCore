using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComProductAttributes")]
    public class ProductAttribute : DomainEntity<Guid>, IMultiLanguage<Guid>
    {
        [Required]
        public Guid ProductId { set; get; }

        [Required]
        public Guid AttributeId { set; get; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        public string Text { set; get; }
    }
}