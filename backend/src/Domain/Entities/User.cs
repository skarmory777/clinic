using System;
using Domain.Enums;
using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FullName { get; private set; }
        public string? ProfilePhoto { get; private set; }
        public List<string> Role { get; private set; } = new();
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiryTime { get; private set; }

        private User() { }

        public User(string username, string email, string passwordHash, string fullName, string profilePhoto, IEnumerable<string> roles)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            ProfilePhoto = profilePhoto;
            Role = roles.ToList(); // converte para List<string>
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string fullName, string email, string? profilePhoto = null, IEnumerable<string>? roles = null)
        {
            FullName = fullName;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
            if (profilePhoto != null)
                ProfilePhoto = profilePhoto;
            if (roles != null)
                Role = roles.ToList(); // converte para List<string>
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void SetRefreshToken(string refreshToken, DateTime expiryTime)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = expiryTime;
        }

        public void RevokeRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiryTime = null;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}