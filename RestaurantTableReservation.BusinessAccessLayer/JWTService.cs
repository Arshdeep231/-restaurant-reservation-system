using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantTableReservation.DataAccessLayer.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantTableReservation.BusinessAccessLayer
{
    public class JWTService
    {
        private IConfiguration _configuration;
        public  JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateJwtToken(User user, string role)
        {
            var _Claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Role,role)
            };
            var _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
            var _Creds = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha256);
            var _Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JWT:ExpireDays"]));
            var Token = new JwtSecurityToken(
            _configuration["JWT:ValidIssuer"],
            _configuration["JWT:ValidAudience"],
            _Claims,
            expires: _Expires,
            signingCredentials: _Creds
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
