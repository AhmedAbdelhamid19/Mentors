using ElMentors.Models.Account;
using ElMentors.Models.Topics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElMentors.Models.Context
{
    public class Context : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Topic> Topic { get; set; }
    }
}
