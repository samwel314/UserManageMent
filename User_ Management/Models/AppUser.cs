using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace User__Management.Models
{
    public class AppUser : IdentityUser
    {
        [Required , MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set;}

        public byte[] ? ImageProfile { get; set; }    
    }
}
