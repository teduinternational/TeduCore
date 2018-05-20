using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComFilters")]
    public class Filter : DomainEntity<Guid>, ISortable
    {
        public Filter()
        {
        }

        public Filter(Guid filterGroupId, int sortOrder)
        {
            FilterGroupId = filterGroupId;
            SortOrder = sortOrder;
        }

        public Guid FilterGroupId { set; get; }
        public int SortOrder { get; set; }
    }
}