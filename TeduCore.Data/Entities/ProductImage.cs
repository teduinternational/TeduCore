using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("ProductImages")]
    public class ProductImage : DomainEntity<int>
    {
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }
    }
}