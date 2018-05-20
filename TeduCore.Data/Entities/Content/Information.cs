using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.Content
{
    [Table("CMS_Informations")]
    public class Information : DomainEntity<Guid>, ISortable, ISwitchable
    {
        public int Bottom { set; get; }
        public int SortOrder { get; set; }
        public Status Status { get; set; }
    }
}