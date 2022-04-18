using Core.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class CompanyUser : BaseEntity
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(50, ErrorMessage = "No more than 50 characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        [MaxLength(50, ErrorMessage = "No more than 50 characters!")]
        public string LastName { get; set; }

        private string _fullname;
        public string FullName
        {
            get
            {
                if (FirstName != null && LastName != null)
                {
                    _fullname = $"{FirstName} {LastName}";
                }
                else
                {
                    _fullname = "Name Surname";
                }
                return _fullname;
            }
        }

        public Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
