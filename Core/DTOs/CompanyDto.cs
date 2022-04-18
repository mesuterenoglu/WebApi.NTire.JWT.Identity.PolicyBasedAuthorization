

using System;
using System.Collections.Generic;

namespace Core.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public virtual List<CompanyUserDto> CompanyUsers { get; set; }
    }
}
