using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("Permissions")]
    public class Permission : DomainEntity<Guid>
    {
        public Permission()
        {
        }

        public Permission(Guid roleId, Guid functionId)
        {
            RoleId = roleId;
            FunctionId = functionId;
        }

        [StringLength(450)]
        [Required]
        public Guid RoleId { get; set; }

        [StringLength(128)]
        [Required]
        public Guid FunctionId { get; set; }

        
    }
}