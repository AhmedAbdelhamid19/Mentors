using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Elmentors.Models
{
    public class Topic
    {
        public Topic()
        {
            Prerequisites = new List<Topic>();
            Dependent = new List<Topic>();
        }
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Topic Name")]
        [MinLength(2, ErrorMessage = "minimum lenght of the name is 2")]
        [UniqueTopicName(Message = "This Name Used Before")]
        [Remote(controller:"Topic",action:"CheckSenstiveWordsAjax", ErrorMessage = "be polite")] // you can send to action addition field by AdditionalFields = "Description"
        public string Name { get; set; }



        [Required(ErrorMessage = "Description is required")]
        [Display(Description = "Type Description  اكتب الوصف")]
        [Remote(controller: "Topic", action: "CheckSenstiveWordsAjax", ErrorMessage = "be polite")]
        public string Description { get; set; }


        public virtual List<Topic>? Prerequisites { get; set; }
        public virtual List<Topic>? Dependent { get; set; }
    }
}
