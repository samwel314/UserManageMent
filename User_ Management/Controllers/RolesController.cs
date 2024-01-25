using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User__Management.ViewModel;

namespace User__Management.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public  IActionResult Index ()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Add (RoleFormVM role)
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Some Erro Happen");
                return View("Index" , _roleManager.Roles.ToList());
            }
            var rolefound = await _roleManager.RoleExistsAsync(role.Name); 
            if (rolefound)
            {
                ModelState.AddModelError("Name", "This Role Is Exists");
                return View("Index" , _roleManager.Roles.ToList());
            }

            await  _roleManager.CreateAsync(new IdentityRole(role.Name.Trim()));
             
         
            return RedirectToAction("Index");
        }
    }
}