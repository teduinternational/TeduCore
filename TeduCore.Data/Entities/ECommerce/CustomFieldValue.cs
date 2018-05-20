using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComCustomFieldValues")]
    public class CustomFieldValue : DomainEntity<Guid>, ISortable
    {
        public CustomFieldValue()
        {
        }

        public CustomFieldValue(Guid customFieldId, int sortOrder)
        {
            CustomFieldId = customFieldId;
            SortOrder = sortOrder;
        }
        [Required]
        public Guid CustomFieldId { set; get; }
        public int SortOrder { get; set; }
    }
}