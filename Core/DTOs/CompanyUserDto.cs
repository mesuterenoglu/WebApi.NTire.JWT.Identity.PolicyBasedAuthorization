

using System;

namespace Core.DTOs
{
    public class CompanyUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
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
        public virtual CompanyDto Company { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUserDto AppUser { get; set; }
    }
}
