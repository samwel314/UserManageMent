using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User__Management.Models;
using User__Management.ViewModel;

namespace User__Management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        
        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  async Task<IActionResult>Index()
        {
            var users = await _userManager.Users
                .Select( c=>new UserVM
                {
                    Id = c.Id,  
                    FirstName = c.FirstName,    
                    LastName = c.LastName,
                    UserName = c.UserName! , 
                    Email = c.Email !,
                   
                }
                ).ToListAsync();
            foreach ( var user in users )
            {

                var C = _userManager.Users.ToList().
                    First(x=>x.Id == user.Id);
                user.Roles = await _userManager.GetRolesAsync(C);
            }
            return View(users);
        }

        public async Task<IActionResult> Manage (string Id )
        {
            var user = await _userManager.FindByIdAsync(Id);
            if ( user == null ) 
            {
                return NotFound();  
            }
            var roles = _roleManager.Roles.ToList();

            var model = new MangeRoleMV
            {
                UserId = user.Id,   
                FirsName= user.FirstName ,
                LastName =  user.LastName,
                Email = user.Email! , 
                UserName = user.UserName!,
                Roles = roles.Select(x=>new RoleMV
                { RoleId = x.Id ,RoleName = x.Name 
                , Isselceted = _userManager.IsInRoleAsync(user, x.Name).Result })
                .ToList(),    
            };
            return View(model);  
        }
        [HttpPost]
        public async Task<IActionResult> Manage(MangeRoleMV model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }
            if (!model.Roles.Any(x => x.Isselceted))
            {
                ModelState.AddModelError("", "Must Chose role");
                return View(model);
            }
            if (user.Email != model.Email)
            {
                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "This  USER Name is used before ");
                    return View(model);
                }
                else
                {
                    user.Email = model.Email;
                }
            }
            if (user.UserName!= model.UserName)
            {
                if (await _userManager.FindByNameAsync(model.UserName) != null)
                {
                    ModelState.AddModelError("UserName", "This  USER Name is used before ");
                    return View(model);
                }
                else
                {
                    user.UserName = model.UserName; 
                }
            }
            var rolesinuser = await _userManager.GetRolesAsync(user);
             foreach (var role in model.Roles)
             {
                 if ( rolesinuser.Any(r => r == role.RoleName) && !role.Isselceted)
                 {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName); 
                 }
                 if (!rolesinuser.Any(r => r == role.RoleName) && role.Isselceted)
                 {
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                 }
             }
            user.FirstName = model.FirsName; 
            user.LastName = model.LastName;

            await _userManager.UpdateAsync(user);  

            return RedirectToAction("index");
        }
 
        public IActionResult Create ()
        {
            var roles = _roleManager.Roles
                .Select(r=> new RoleMV
                {
                    RoleName = r.Name! ,
                    RoleId = r.Id , 
                }).ToList();

            var UserModel = new AddUserVM
            {
                Roles = roles
            }; 


            return View(UserModel);  
        }
        [HttpPost]
        public async Task <IActionResult> Create(AddUserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.Roles.Any(x=>x.Isselceted))
            {
                ModelState.AddModelError("", "Must Chose role");
                return View(model);
            }
            if (await _userManager.FindByEmailAsync(model.Email)!= null)                
            {
                ModelState.AddModelError("Email", "This USER Name is used before ");
                return View(model);
            }
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "This  USER Name is used before ");
                return View(model);
            }
            AppUser appUser = new AppUser()
            {
                FirstName = model.FirsName,
                LastName = model.FirsName,
                Email = model.Email,
                UserName = model.UserName
            };
           var userCreated = await _userManager.CreateAsync(appUser, model.Password);
            if (userCreated.Succeeded)
            {
                await _userManager.AddToRolesAsync
                    (appUser,model.Roles.Where(x=>x.Isselceted).Select(x=>x.RoleName) );
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in userCreated.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    
                }
                return View(model);
            }
        }
    }
}
