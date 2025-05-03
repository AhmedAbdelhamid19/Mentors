using Elmentors.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Elmentors.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole<int>> roleManager;
        public RoleController(RoleManager<IdentityRole<int>> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleViewModel role)
        {
            if(ModelState.IsValid)
            {
                IdentityRole<int> roleModel = new IdentityRole<int>();
                roleModel.Name = role.RoleName;
                IdentityResult result= await roleManager.CreateAsync(roleModel);
                if(result.Succeeded)
                {
                    ViewBag.Success = true;
                    return View("AddRole", new RoleViewModel());
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("AddRole" ,role);
        }
    }
}
