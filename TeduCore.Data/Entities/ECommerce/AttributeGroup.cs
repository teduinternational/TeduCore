using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComAttributeGroups")]
    public class AttributeGroup : DomainEntity<Guid>, ISortable
    {
        public AttributeGroup()
        {
        }
        public AttributeGroup(int sortOrder)
        {
            SortOrder = sortOrder;
        }

        

        public int SortOrder { get; set; }
    }
}