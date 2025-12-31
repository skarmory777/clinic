using System;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interfaces;
using Application.DTOs.User;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !user.IsActive)
                throw new UnauthorizedAccessException("Credenciais inválidas");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Credenciais inválidas");

            var token = _jwtService.GenerateToken(user.Id, user.Username, user.Email, new[] { "User" });
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));
            await _userRepository.UpdateAsync(user);

            return new LoginResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(30),
                User = new UserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt
                }
            };
        }

        public async Task<LoginResponse> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(token);
            var userId = Guid.Parse(principal.FindFirst(JwtRegisteredClaimNames.Sub)!.Value);
            
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new SecurityTokenException("Refresh token inválido");

            var newToken = _jwtService.GenerateToken(user.Id, user.Username, user.Email, new[] { "User" });
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.SetRefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(7));
            await _userRepository.UpdateAsync(user);

            return new LoginResponse
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(30),
                User = new UserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt
                }
            };
        }

        public async Task<bool> LogoutAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.RevokeRefreshToken();
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            return Task.FromResult(_jwtService.ValidateToken(token));
        }
    }
}