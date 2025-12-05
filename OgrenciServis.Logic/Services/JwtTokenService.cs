using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace OgrenciServis.Logic.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            // Secret key oku
            var secretKey = _configuration["Jwt:SecretKey"];
            // Secret key byte dizisine çevir
            var key = Encoding.UTF8.GetBytes(secretKey);

            // Token tanımalama
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Claim bilgileri ekle
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("UserId", user.UserId.ToString()),
                    new System.Security.Claims.Claim("UserName", user.UserName),
                    new System.Security.Claims.Claim("Role", user.Role),
                    new System.Security.Claims.Claim("EMail","ertugrulkra@gmail.com"),
                    new System.Security.Claims.Claim("Phone","555 623 67 63"),
                    new System.Security.Claims.Claim("CName","Ertugrul-PCX"),
                }),

                //Token Suresi
                Expires = DateTime.UtcNow.AddDays(1),

                //Imzalama
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            // Token oluşturma adımı
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Token'ı string olarak döndür
            return tokenHandler.WriteToken(token);
        }
    }
}
