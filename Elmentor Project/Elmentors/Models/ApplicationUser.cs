using Microsoft.AspNetCore.Identity;

namespace Elmentors.Models
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string? Address { get; set; }
    }
}
