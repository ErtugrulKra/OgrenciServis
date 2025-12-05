using OgrenciServis.DataAccess;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly OkulContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(OkulContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public LoginResponseDto? Login(LoginRequestDto loginRequest)
        {
            //1. kullanıcı adı ve şifre ile veritabanın user sorgula
            //var user = _context.Users.FirstOrDefault(u => u.UserName == loginRequest.Username);

            var user = (from u in _context.Users
                        where u.UserName == loginRequest.Username
                        select u).FirstOrDefault();

            //2. kullanıcı bulunamazsa null döndür
            if (user == null)
                return null;

            //Sifre kontrolü (basit örnek)
            if (user.Password != loginRequest.Password)
                return null;

            //3. kullanıcı bulunursa jwt token oluştur
            var token = _jwtTokenService.GenerateToken(user);

            //4. LoginResponseDto oluştur ve döndür
            return new LoginResponseDto
            {
                Token = token,
                Username = user.UserName,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddDays(1) //örnek olarak 1 gun geçerli
            };

        }
    }
}
