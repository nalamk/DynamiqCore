using DynamiqCore.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DynamiqCore.Application.JWT
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        #region Fields
        
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        #endregion

        #region IJwtTokenGenerator Implementation

        public string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user.Id),
                    new(JwtRegisteredClaimNames.Email, user.Email),
                    new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(1),
                    signingCredentials: creds);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return jwtToken;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            
        }

        #endregion

        
    }
}
