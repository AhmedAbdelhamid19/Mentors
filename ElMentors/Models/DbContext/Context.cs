using ElMentors.Models.Account;
using ElMentors.Models.Tests;
using ElMentors.Models.Topics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ElMentors.Models.Context
{
    public class Context : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public override int SaveChanges()
        {
            var Entites = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach(var Entity in Entites)
            {
                Validator.ValidateObject(Entity, new ValidationContext(Entity), true);
            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Topic> Topic { get; set; }
        public DbSet<Test1> Test1 { get; set; }
        public DbSet<Test2> Test2 { get; set; }

    }
}
