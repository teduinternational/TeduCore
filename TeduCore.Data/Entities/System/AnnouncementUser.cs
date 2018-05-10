using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("AnnouncementUsers")]
    public class AnnouncementUser : DomainEntity<Guid>
    {
        [Required]
        public Guid AnnouncementId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public bool? HasRead { get; set; }
    }
}