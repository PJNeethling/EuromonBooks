using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Services.JwtService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EuromonBooks.Domain.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> GenerateJwtToken(UserAccessInfo user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Uuid", user.Uuid.ToString()),
                new Claim("Roles", string.Join(",", user.Roles.Select(x => x.Id.ToString())))
            };

            var token = new JwtSecurityToken(_options.JwtIssuer,
              _options.JwtAudience,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}