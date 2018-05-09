using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("Errors")]
    public class Error : DomainEntity<string>
    {
        public string Message { set; get; }

        public string StackTrace { set; get; }

        public DateTime CreatedDate { set; get; }
    }
}