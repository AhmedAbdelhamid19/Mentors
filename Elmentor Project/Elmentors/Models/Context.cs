using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Elmentors.Models
{
    public class Context: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>

    {
        public Context(): base() { }
        public Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<RoadMap> RoadMaps { get; set; }

    }
}
