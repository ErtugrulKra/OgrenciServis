using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciController : ControllerBase
    {
        private readonly IOgrenci ogrenci;

        //Dependecy Injection
        public OgrenciController(IOgrenci ogrenci)
        {
            this.ogrenci = ogrenci;
        }

        [HttpGet]
        public ActionResult<OgrenciDto> GetOgrenciler()
        {
            return Ok(this.ogrenci.TumOgrencileriListele());
        }
    }
}
