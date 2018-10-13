using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Google.Apis.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace postgres_service.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ILogger _logger;
        private readonly Repository _repository;
        public AuthController(ILogger<AuthController> logger, Repository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        [Route("google/{token}")]
        [HttpGet]
        public async Task<IActionResult> LoginWithGoogleToken(string token)
        {
            _logger.LogInformation(token);
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);

            var user = _repository.Users.SingleOrDefault(u => u.GoogleUserId == payload.Subject);
            if (user == null) {
                user = new User {
                    GoogleUserId = payload.Subject,
                    Email = payload.Email,
                    GivenName = payload.GivenName,
                    FamilyName = payload.FamilyName,
                    Roles = new string[0]
                };
                _repository.Users.Add(user);
                _repository.SaveChanges();
            }

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.GivenName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.FamilyName)
            };
            claims = claims.Concat(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.ToString()))).ToArray();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken("issuer", "issuer", claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwtToken));
        }
    }
}
