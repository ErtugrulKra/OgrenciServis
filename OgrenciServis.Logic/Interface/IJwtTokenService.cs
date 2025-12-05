using OgrenciServis.Models;

namespace OgrenciServis.Logic.Interface
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
