﻿namespace IAM.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
