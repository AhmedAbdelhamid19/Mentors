using Microsoft.AspNetCore.Identity;

namespace ElMentors.Models.Account
{
    public class ApplicationUser: IdentityUser<string>
    {
        public string? PhotoPath { get; set; }
    }
}
