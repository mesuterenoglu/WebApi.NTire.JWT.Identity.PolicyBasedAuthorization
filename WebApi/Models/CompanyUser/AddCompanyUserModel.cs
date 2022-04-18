

using Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.CompanyUser
{
    public class AddCompanyUserModel
    {
        [Required(ErrorMessage = "İsim girişi zorunludur!")]
        [MaxLength(50, ErrorMessage = "50 karakterden fazla olmaz")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim girişi zorunludur!")]
        [MaxLength(50, ErrorMessage = "50 karakterden fazla olmaz")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [MaxLength(50, ErrorMessage = "No more than 50 characters!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Password]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required!")]
        [Compare("Password",ErrorMessage ="Password and confirm has to be equal!")]
        public string ConfirmPassword { get; set; }
    }
}
