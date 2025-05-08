using Microsoft.AspNetCore.Mvc;
using ElMentors.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ElMentors.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> userManager { get; set; }
        public RoleManager<ApplicationRole> roleManager { get; set; }
        public SignInManager<ApplicationUser> signInManager { get; set; }
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> UserManager, RoleManager<ApplicationRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = UserManager;
            this.roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View("SignUp");
        }
        [HttpPost] 
        public async Task<IActionResult> SaveSignUp(SignUpViewModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = model.Email;
                user.UserName = model.Handle;
                user.Id = Guid.NewGuid().ToString();

                // here put password to hash, if not add it as field like Email and UserName
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Student");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("ViewTopics", "Topic");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("SignUp",model);
        }


        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveRole(string RoleName)
        {
            ApplicationRole role = new ApplicationRole();
            role.Name = RoleName;
            role.Id = Guid.NewGuid().ToString();

            await roleManager.CreateAsync(role);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveLogIn(LoginViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.Handle);

            if(user != null)
            {
                bool flag = await userManager.CheckPasswordAsync(user, model.Password);
                if(flag)
                {
                    List<Claim> claims = new List<Claim>() { new Claim("Handle", model.Handle) };

                    await signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);
                    //await signInManager.SignInAsync(user, model.RememberMe);

                    return RedirectToAction("ViewTopics", "Topic");
                }
            }

            ModelState.AddModelError("", "handle or password is wrong");
            return View("LogIn", model);
        }


        [HttpGet] 
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
