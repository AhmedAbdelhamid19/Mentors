using System.ComponentModel.DataAnnotations;

namespace ElMentors.Models.Topics
{
    public class Topic
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Topic Name")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters long")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Topic Description")]
        [StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters long")]
        public string Description { get; set; }


        public virtual ICollection<Topic> Prerequisites { get; set; } = new HashSet<Topic>();
        public virtual ICollection<Topic> Dependents { get; set; } = new HashSet<Topic>();
    }
}
