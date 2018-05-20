using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComCustomFieldCustomerGroups")]
    public class CustomFieldCustomerGroup : DomainEntity<Guid>
    {
        public Guid CustomFieldId { set; get; }
        public Guid CustomerGroupId { set; get; }
        public bool Required { set; get; }
    }
}