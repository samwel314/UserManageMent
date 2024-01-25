using System.ComponentModel.DataAnnotations;

namespace User__Management.ViewModel
{
    public class MangeRoleMV
    {
        public string UserId { get; set;}
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]

        [Display(Name = "Firs Name")]
        public string FirsName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]

        [Display(Name = "LastName")]
        public string LastName { get; set; }


        [Display(Name = "UserName")]
        public string UserName { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public List<RoleMV> Roles { get; set; } 

    }
}
