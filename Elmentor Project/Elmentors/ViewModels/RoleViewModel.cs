using System.ComponentModel.DataAnnotations;

namespace Elmentors.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
