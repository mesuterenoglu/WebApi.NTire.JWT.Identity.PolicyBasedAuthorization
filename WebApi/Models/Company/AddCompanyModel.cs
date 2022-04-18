

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Company
{
    public class AddCompanyModel
    {
        [Required]
        [MaxLength(500, ErrorMessage = "Can not be more than 500 charachters")]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Can not be more than 250 charachters")]
        public string Address { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}
