using System;       
using System.Collections.Generic;         

namespace Application.DTOs.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? ProfilePhoto { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<string> Role { get; set; } = new List<string>();        
        public DateTime CreatedAt { get; set; }
    }
}