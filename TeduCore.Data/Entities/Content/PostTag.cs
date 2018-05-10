using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("BlogTags")]
    public class PostTag : DomainEntity<Guid>
    {
        public Guid PostId { set; get; }

        public string TagId { set; get; }

    }
}