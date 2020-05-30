using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.Identity.RequestModels;
using Domain.Model.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtSettings> jwtSettingsOptionsMonitor)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettingsOptionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Token")]
        public async Task<IActionResult> GetToken([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(new { Message = "Invalid user or password" });

            var identityUser = await GetIdentityUser(loginRequest);
            if (identityUser == null)
                return new BadRequestObjectResult(new { Message = "Unknown user or password" });

            var token = GenerateToken(identityUser);

            return Ok(new { Token = token, Message = "Success" });
        }

        private object GenerateToken(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName),
                    new Claim(ClaimTypes.Email, identityUser.Email),
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                    SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.ExpiryTimeInSeconds),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<IdentityUser> GetIdentityUser(LoginRequest loginRequest)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginRequest.User);
            if (identityUser == null)
                return null;

            var passwordVerificationResult = _userManager.PasswordHasher
                .VerifyHashedPassword(identityUser, identityUser.PasswordHash, loginRequest.Password);

            return passwordVerificationResult == PasswordVerificationResult.Failed ? null : identityUser;
        }
    }
}
