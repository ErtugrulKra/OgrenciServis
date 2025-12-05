using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Interface
{
    public interface IAuthService
    {
        LoginResponseDto? Login(LoginRequestDto loginRequest);
    }
}
