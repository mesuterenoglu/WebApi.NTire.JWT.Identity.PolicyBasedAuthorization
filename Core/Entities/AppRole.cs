

using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Entities
{
    public class AppRole : IdentityRole<string>
    {
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
