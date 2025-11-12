using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;

namespace OgrenciServis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgretmenController : ControllerBase
    {
        private readonly IOgretmen ogretmen;

        public OgretmenController(IOgretmen ogretmen)
        {
            this.ogretmen = ogretmen;
        }

        [HttpGet]   
        public ActionResult GetOgretmenler()
        {
            return Ok(this.ogretmen.TumOgretmenleriListele());
        }
    }
}
