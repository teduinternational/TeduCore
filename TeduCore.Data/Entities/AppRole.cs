using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduCore.Data.Entities
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<string>
    {
        public AppRole() : base()
        {
        }

        public AppRole(string name, string description) : base(name)
        {
            this.Description = description;
        }

        [StringLength(250)]
        public string Description { get; set; }
    }
}