using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("Announcements")]
    public class Announcement : DomainEntity<Guid>, ISwitchable, IDateTracking, IHasOwner<Guid>
    {
        [Required]
        [StringLength(250)]
        public string Title { set; get; }

        [StringLength(250)]
        public string Content { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }

        public DateTime? DateDeleted { set; get; }
        public Status Status { set; get; }
        public Guid OwnerId { set; get; }
    }
}