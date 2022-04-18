

using Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.User
{
    public class NewPasswordModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [MaxLength(50, ErrorMessage = "No more than 50 characters!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Current password is required!")]
        [Password]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required!")]
        [Password]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required!")]
        [Password]
        [Compare("NewPassword", ErrorMessage = "New password and confirm has to be equal!")]
        public string ConfirmNewPassword { get; set; }


    }
}
