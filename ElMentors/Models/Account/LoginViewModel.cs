using System.ComponentModel.DataAnnotations;

namespace ElMentors.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string Handle { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
