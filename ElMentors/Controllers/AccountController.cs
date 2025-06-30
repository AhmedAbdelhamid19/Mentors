using Microsoft.AspNetCore.Mvc;
using ElMentors.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                    var photoPath = user.PhotoPath ?? string.Empty;
                    List<Claim> claims = new List<Claim>() {
                        new Claim("Handle", model.Handle),
                        new Claim("PhotoPath", photoPath)
                    };

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

        [HttpGet]
        public IActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("LogIn");
                }
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                user.PhotoPath = "/uploads/" + uniqueFileName;
                await userManager.UpdateAsync(user);

                // Update PhotoPath claim and re-sign in
                var claims = new List<Claim>() {
                    new Claim("Handle", user.UserName),
                    new Claim("PhotoPath", user.PhotoPath ?? string.Empty)
                };
                await signInManager.SignOutAsync();
                await signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);

                ViewBag.Message = "Photo uploaded successfully!";
                return View();
            }
            ViewBag.Message = "Please select a valid photo.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("LogIn");
            }
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser(string UserId, string RoleName)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user != null && !string.IsNullOrEmpty(RoleName))
            {
                await userManager.AddToRoleAsync(user, RoleName);
                TempData["Message"] = $"Role '{RoleName}' assigned to user '{user.UserName}'.";
            }
            return RedirectToAction("AddRole");
        }
    }
}
