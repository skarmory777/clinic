using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string username, string email, IEnumerable<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool ValidateToken(string token);
    }
}