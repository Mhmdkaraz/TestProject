using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TestProject.Models;

namespace TestProject.Helpers {
    public class TokenService : ITokenService {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config) {
            _config = config;
        }
        public string CreateToken(User user) {
            List<Claim> claims = new List<Claim>{
             new Claim(ClaimTypes.Name, user.Email),
             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

         };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public User ValidateToken(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                return new User { Id = Guid.Parse(userId) };
            } catch {
                return null;
            }
        }
    }
}
