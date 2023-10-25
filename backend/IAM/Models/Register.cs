using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IAM.Models
{
    public enum UserRole
    {
        Admin,
        User
    }

    public class Register
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
    }
}