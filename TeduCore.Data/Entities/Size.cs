﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("Sizes")]
    public class Size : DomainEntity<int>
    {
        [StringLength(250)]
        public string Name
        {
            get; set;
        }
    }
}