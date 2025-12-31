using System;
using System.Threading.Tasks;
using Application.DTOs.Auth;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse> RefreshTokenAsync(string token, string refreshToken);
        Task<bool> LogoutAsync(Guid userId);
        Task<bool> ValidateTokenAsync(string token);
    }
}