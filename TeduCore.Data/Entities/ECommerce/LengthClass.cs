using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComLengthClass")]
    public class LengthClass : DomainEntity<Guid>
    {
        public decimal Value { set; get; }
    }
}