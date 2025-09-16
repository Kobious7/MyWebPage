using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WithDB.Common.Responses;
using WithDB.DTOs.Login;
using WithDB.Models;

namespace WithDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Users.AnyAsync( u => u.UserName == dto.UserName ))
                return BadRequest(ApiResponse<string>.Fail("Username already exists"));

            var hasher = new PasswordHasher<string>();
            var hash = hasher.HashPassword(dto.UserName, dto.Password);

            var user = new User
            { 
                UserName = dto.UserName,
                PasswordHash = hash,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<string>.Ok(null, "Register sucessfully"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
                return Unauthorized(ApiResponse<string>.Fail($"{dto.UserName} was not found"));

            var hasher = new PasswordHasher<string>();
            var result = hasher.VerifyHashedPassword(dto.UserName, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(ApiResponse<string>.Fail("Password is invalid"));

            var token = GenerateJwtToken(user);

            return Ok(ApiResponse<object>.Ok(new
            {
                token
            }, "Login successfully"));
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("this_is_a_super_secret_key_that_is_at_least_32_chars"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "myapp",
                audience: "myappuser",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
           
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
