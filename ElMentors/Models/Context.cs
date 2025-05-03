using ElMentors.Models.Topics;
using Microsoft.EntityFrameworkCore;

namespace ElMentors.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Topic> Topic { get; set; }
    }
}
