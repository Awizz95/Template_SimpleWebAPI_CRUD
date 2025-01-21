using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Template_SimpleWebAPI_CRUD.Models;

namespace Template_SimpleWebAPI_CRUD.Infrastructure.Services
{
    public class TokenService 
    {
        private const int ExpirationInMinutes = 60;
        private readonly ILogger<TokenService> _logger;
        private readonly IConfiguration _configuration;

        public TokenService(ILogger<TokenService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string CreateToken(AppUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationInMinutes);
            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCreadentials(),
                expiration);

            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInformation("JWT token created!");

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
        {
            return new JwtSecurityToken(
                issuer: _configuration["AppSettings:JwtOptions:Issuer"],
                audience: _configuration["AppSettings:JwtOptions:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);
        }

        private List<Claim> CreateClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            return claims;
        }

        private SigningCredentials CreateSigningCreadentials()
        {
            var symmetricSecurityKey = Encoding.UTF8.GetBytes(_configuration["AppSettings:JwtOptions:SigningKey"]!);

            return new SigningCredentials(new SymmetricSecurityKey(symmetricSecurityKey), SecurityAlgorithms.HmacSha256);
        }
    }
}
