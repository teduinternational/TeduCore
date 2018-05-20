using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComCustomFieldValueDescriptions")]
    public class CustomFieldValueDescription : DomainEntity<Guid>, IMultiLanguage<Guid>
    {
        [Required]
        public Guid CustomFieldValueId { set; get; }

        [Required]
        public Guid LanguageId { set; get; }

        [Required]
        public Guid CustomFieldId { set; get; }

        [MaxLength(256)]
        public string Name { set; get; }
    }
}