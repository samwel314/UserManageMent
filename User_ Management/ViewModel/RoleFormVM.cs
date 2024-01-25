using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace User__Management.ViewModel
{
    public class RoleFormVM
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }        

    }
}
