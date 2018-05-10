using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("AdvertistmentPages")]
    public class AdvertistmentPage : DomainEntity<Guid>,IHasUniqueCode
    {
        public string UniqueCode { get; set; }
        public string Name { get; set; }
       
    }
}