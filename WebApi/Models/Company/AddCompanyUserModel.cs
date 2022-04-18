

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Company
{
    public class AddCompanyUserModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [MaxLength(50, ErrorMessage = "No more than 50 characters!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
