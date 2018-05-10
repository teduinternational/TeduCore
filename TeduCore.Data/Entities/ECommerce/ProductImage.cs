using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("ProductImages")]
    public class ProductImage : DomainEntity<Guid>,ISortable
    {
        public Guid ProductId { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }

        public int SortOrder { get; set; }
    }
}