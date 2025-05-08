using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

namespace ElMentors.Models.Account
{
    public class SignUpViewModel
    {
        [Required]
        public string Handle { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
