using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("ProductTags")]
    public class ProductTag : DomainEntity<Guid>
    {
        public Guid ProductId { set; get; }

        public string TagId { set; get; }
    }
}