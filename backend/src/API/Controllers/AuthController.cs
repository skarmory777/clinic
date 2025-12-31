using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var result = await _authService.RefreshTokenAsync(request.Token, request.RefreshToken);
                return Ok(result);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value!);
            await _authService.LogoutAsync(userId);
            return Ok(new { message = "Logout realizado com sucesso" });
        }

        [HttpGet("validate")]
        [Authorize]
        public async Task<IActionResult> ValidateToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var isValid = await _authService.ValidateTokenAsync(token);
            return Ok(new { valid = isValid });
        }
    }

    public class RefreshTokenRequest
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}