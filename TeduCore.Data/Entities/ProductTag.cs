using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("ProductTags")]
    public class ProductTag : DomainEntity<int>
    {
        [Column(Order = 1)]
        public int ProductId { set; get; }

        public string TagId { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}