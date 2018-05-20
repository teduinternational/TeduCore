using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComOrderStatus")]
    public class OrderStatus : DomainEntity<Guid>, IMultiLanguage<Guid>
    {
        [MaxLength(32)]
        public string Name { set; get; }

        public Guid LanguageId { get; set; }
    }
}