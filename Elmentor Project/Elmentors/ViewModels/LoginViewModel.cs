using System.ComponentModel.DataAnnotations;

namespace Elmentors.ViewModels
{
    public class LoginViewModel
    {   
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
