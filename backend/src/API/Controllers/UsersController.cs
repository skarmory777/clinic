using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs.User;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UsersController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpGet("me")]
        [Authorize] // garante que só usuários autenticados chegam aqui
        public async Task<ActionResult<UserResponse>> GetCurrentUser()
        {
            // Tenta pegar a claim "sub"
            var subClaim = User.FindFirst("sub");
            if (subClaim == null || !Guid.TryParse(subClaim.Value, out var userId))
            {
                // Token inválido ou claim não encontrada
                return Unauthorized("Token inválido ou claim 'sub' ausente.");
            }

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null) return NotFound("Usuário não encontrado.");

            return Ok(new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            });
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterRequest request)
        {
            if (await _userRepository.ExistsByEmailAsync(request.Email))
                return Conflict(new { message = "Email já cadastrado" });

            if (await _userRepository.ExistsByUsernameAsync(request.Username))
                return Conflict(new { message = "Username já cadastrado" });

            var user = new User(request.Username, request.Email, "", request.FullName, request.ProfilePhoto, request.Roles ?? new List<string>());
            var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
            user.SetPasswordHash(hashedPassword);

            await _userRepository.AddAsync(user);

            return CreatedAtAction(nameof(GetCurrentUser), new { id = user.Id }, new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            });
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? ProfilePhoto { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}