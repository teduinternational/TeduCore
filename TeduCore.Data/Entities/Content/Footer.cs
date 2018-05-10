using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("Footers")]
    public class Footer : DomainEntity<string>, ISwitchable
    {
        [Required]
        public string Content { set; get; }

        public Status Status { set; get; }
    }
}