using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComOptionDescriptions")]
    public class OptionDescription : DomainEntity<Guid>, IMultiLanguage<Guid>
    {
        [Required]
        public Guid OptionId { set; get; }

        [Required]
        public Guid LanguageId { get; set; }

        [MaxLength(128)]
        [Required]
        public string Name { set; get; }
    }
}