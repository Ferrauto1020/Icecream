using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IcecreamApp.Shared.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace IcecreamApp.Api.Services
{
    public class TokenServices(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration) =>
            new()
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = GetSecurityKey(configuration)
            };


        public string GenerateJwt(LoggedInUser user)
        {
            var securityKey = GetSecurityKey(_configuration);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var issuer = _configuration["Jwt:Issuer"];
            var expireInMinute = Convert.ToInt32(_configuration["Jwt:ExpireInMinute"]);

            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.StreetAddress,user.Address),
            ];

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: "*",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireInMinute),
                signingCredentials: credentials
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private static SymmetricSecurityKey GetSecurityKey(IConfiguration configuration)
        { 
                var secretKey = configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        return securityKey;
        }
    }
}