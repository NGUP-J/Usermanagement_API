using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Usermanagement_API.Repositories.Interface;

namespace Usermanagement_API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration) 
        { 
            this.configuration = configuration;
        }


        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Jwt Security Token Parameters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                               issuer: configuration["Jwt:Issuer"],
                               audience: configuration["Jwt:Audience"],
                               claims: claims,
                               expires: DateTime.Now.AddMinutes(15),
                               signingCredentials: creadentials);
            // Return Token

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
