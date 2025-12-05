using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto loginRequest)
        {
            //Bilgileri dogrula
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Login islemini yap
            var result = _authService.Login(loginRequest);

            if (result == null)
                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");

            //Token bilgilerini don
            return Ok(result);

        }
    }
}
