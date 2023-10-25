using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IAM.Models
{
    public class AppUser : IdentityUser
    {
        public bool IsActive { get; set; }
    }
}