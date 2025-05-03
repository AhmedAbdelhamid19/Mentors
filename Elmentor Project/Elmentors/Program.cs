using Elmentors.Filters;
using Elmentors.Models;
using Elmentors.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// to logs in file 
using Serilog;

namespace Elmentors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region to make filter global
            //builder.Services.AddControllersWithViews(
            //    options =>
            //    {
            //        options.Filters.Add(new ClassName());
            //    }
            //);
            #endregion
            #region to use dependency injection
            builder.Services.AddScoped<CreditCardPaymentService>();
            builder.Services.AddScoped<PayPalPaymentService>();
            builder.Services.AddScoped<ITopicRepository, TopicRepository>();
            #endregion
            #region to register the Authorization IdentityUser and IdentityRole
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(
                option =>
                {
                    option.Password.RequiredLength = 4;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireDigit = false;
                }
            ).AddEntityFrameworkStores<Context>();
            #endregion
            #region Cookies options
            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Ensure the session expires as soon as the browser closes
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Make sure the session ends after 1 minute of inactivity
                //options.SlidingExpiration = false;
            });
            #endregion
            #region add session
            builder.Services.AddSession(
                options=>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(1);
                }
            );
            #endregion
            #region set database 
            builder.Services.AddDbContext<Context>(
                option =>
                {
                    option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
                }
            );
            #endregion

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
