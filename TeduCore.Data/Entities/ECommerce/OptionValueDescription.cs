using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComOptionValueDescriptions")]
    public class OptionValueDescription : DomainEntity<Guid>, IMultiLanguage<Guid>
    {
        [Required]
        public Guid OptionValueId { set; get; }

        [Required]
        public Guid OptionId { set; get; }

        [Required]
        public Guid LanguageId { get; set; }

        [MaxLength(128)]
        public string Name { set; get; }
    }
}