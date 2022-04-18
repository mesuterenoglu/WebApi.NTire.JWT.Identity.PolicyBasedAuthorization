using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Entities
{
    public class AppUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
