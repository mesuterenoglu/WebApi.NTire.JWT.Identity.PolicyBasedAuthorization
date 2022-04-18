

using Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.User
{
    public class NewEmailModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [MaxLength(50, ErrorMessage = "No more than 50 characters!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Compare("Password", ErrorMessage = "Password and confirm has to be equal!")]
        [Password]
        public string Password { get; set; }

        [Required(ErrorMessage = "New email is required!")]
        [MaxLength(50, ErrorMessage = "No more than 50 characters!")]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
