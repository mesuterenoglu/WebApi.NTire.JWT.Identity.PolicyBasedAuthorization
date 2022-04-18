using Core.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Company : BaseEntity
    {
        [Required]
        [MaxLength(500, ErrorMessage = "Can not be more than 500 charachters")]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Can not be more than 250 charachters")]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public virtual List<CompanyUser> CompanyUsers { get; set; }
    }
}
