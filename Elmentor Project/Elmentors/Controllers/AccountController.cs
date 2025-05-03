using Elmentors.Models;
using Elmentors.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;

namespace Elmentors.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterViewModel register)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = register.UserName;
                user.Address = register.Address;
                 
                // IdentityResult result = await userManager.CreateAsync(user); // if you don't want to hash password
                IdentityResult result = await userManager.CreateAsync(user, register.Password); // to add new user to the database

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("ViewTopics", "Topic");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", register);
        }
        public async  Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View("Register");
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> SaveLogin(LoginViewModel login)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(login.Name);
                if(user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, login.Password);
                    if(found)
                    {
                        #region add new claims to cookie with Name and Id
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Address", user.Address));
                        await signInManager.SignInWithClaimsAsync(user, login.RememberMe, claims);
                        #endregion
                        #region add Name and Id only to Cookie
                        //await signInManager.SignInAsync(user, login.RememberMe);
                        #endregion
                        return RedirectToAction("ViewTopics", "Topic");
                    }
                }

                ModelState.AddModelError("", "User Name or Password is wrong");
            }
            return View("login", login);
        }
    }
}
