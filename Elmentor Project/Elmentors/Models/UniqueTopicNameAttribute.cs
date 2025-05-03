using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Elmentors.Models
{
    public class UniqueTopicNameAttribute : ValidationAttribute
    {
        public string? Message {get; set;}
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return null;

            string? Name = value.ToString();

            Context context = new Context();
            Topic topic = context.Topics.FirstOrDefault(t => t.Name == Name);
            if(topic != null)
            {
                return new ValidationResult( Message != null ? Message : "Try Again, name of the topic used before");
            }
            return ValidationResult.Success;
        }


    }
}
