﻿using Microsoft.AspNetCore.Identity;

namespace ElMentors.Models.Account
{
    public class ApplicationRole: IdentityRole<string>
    {
        public string? Description { get; set; }
    }
}
