using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class UserModel : IValidatableObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UserValidation.IsValidEmail(EmailAddress))
            {
                yield return new ValidationResult("Email must be \"....@gmail.com\"");
            }

            if (!UserValidation.IsValidPassword(Password))
            {
                yield return new ValidationResult("The password must contain exactly one uppercase, one lowercase, and at least eight numbers ");
            }

        }
    }
}
