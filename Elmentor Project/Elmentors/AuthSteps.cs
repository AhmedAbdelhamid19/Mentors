/*
 * 1) Install Identity package [NuGet >> Microsoft.AspNetCore.Identity.EntityFrameworkCore >> downlad correct version]
 * 2) Create 
 *      - ApplicationUser: IdentityUser
 *      - OR ApplicationUser: IdentityUser<int> 
 *      - [to add new attribute], [also if you won't add make it too]
 *  
 * 3) Change Context as following:
 *      - Context: IdentityDbContext<ApplicationUser>
 *      - OR Context: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
 * 4) this line in in Program.cs: 
 *      - builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>();
 *      - builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>().AddEntityFrameworkStores<Context>();
 *      - you can add option here like following:
 *          - builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>();
 *      - app.UseAuthentication();
 * 5) in console to update database
 *      - Add-migration
 *      - update-database
 * 6) make AccountController that handle Actions like register,login and it's View and ViewModel(use vs help) and the Action that take inf. from the form SafeRegister,SafeLogin
 * 7) make in AccountController
 *      - make UserManger (with Dependency Injection)
 *      - make SignInManger (With Dependency Injection)  
 * 8) in SaveRegister 
 *      - make {ApplicationUser user;}  obj and fill it with the ViewModel sent from the View Form
 *      - add the user to the db (remember UserManger is the service that deal with database)
 *          - IdentityResult result = await userManager.CreateAsync(user)
 *      - make cookie if the adding success and user Required it in RememberMe 
 *          - if(result.success) 
 *              await signInManger.SigninAsync(ApplicationUser, ViewModel.RememberMe)
 *      - Add Errors in ModelState if result.success is false >> loop in result.errors >> ModelResult.AddModelError("",error.Description)
 * 9) in SaveLogin: chekc if Handle(Name,email...etc) exist then if the password correct
 *      - check handle exist >> {ApplicationUser user = await userManager.FindByNameAsync(login.Name);}
 *      - check password correct >> in userManger, CheckPasswordAsync and if you want to save cookie then SignInAsync
 *      - don't forget to put in ModelState.AddModelError "Password or handle isn't correct" if it ordered 
 *              
 * 
 *  
 */