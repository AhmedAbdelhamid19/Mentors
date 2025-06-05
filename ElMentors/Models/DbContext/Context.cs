using ElMentors.Models.Account;
using ElMentors.Models.Tests;
using ElMentors.Models.Topics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MidTest>()
                .HasKey(mt => new { mt.Test1Id, mt.Test2Id });
            modelBuilder.Entity<MidTest>()
                .HasOne(mt => mt.Test1)
                .WithMany(t1 => t1.mids)
                .HasForeignKey(mt => mt.Test1Id);
            modelBuilder.Entity<MidTest>()
                .HasOne(mt => mt.Test2)
                .WithMany(t2 => t2.mids)
                .HasForeignKey(mt => mt.Test2Id);

            modelBuilder.Entity<parent>().UseTpcMappingStrategy();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Topic> Topic { get; set; }
        public DbSet<Test1> Test1 { get; set; }
        public DbSet<Test2> Test2 { get; set; }
        public DbSet<MidTest> MidTests { get; set; }

        public DbSet<parent> Parents { get; set; }
        public DbSet<child1> child1s { get; set; }

        public DbSet<child2> child2s { get; set; }
    }
}
