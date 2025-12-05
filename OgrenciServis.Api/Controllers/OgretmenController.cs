using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OgretmenController : ControllerBase
    {
        private readonly IOgretmen ogretmen;

        public OgretmenController(IOgretmen ogretmen)
        {
            this.ogretmen = ogretmen;
        }

        // GET: api/Ogretmen
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<OgretmenDto>> GetOgretmenler()
        {
            return Ok(this.ogretmen.TumOgretmenleriListele());
        }

        // GET: api/Ogretmen/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Ogretmen")]
        public ActionResult<OgretmenDto> GetOgretmen(int id)
        {
            var ogretmenDto = this.ogretmen.OgretmenGetirById(id);

            if (ogretmenDto == null)
            {
                return NotFound($"Öğretmen ID {id} bulunamadı.");
            }

            return Ok(ogretmenDto);
        }

        // POST: api/Ogretmen
        [HttpPost]
        [Authorize]
        public ActionResult<Ogretmen> PostOgretmen([FromBody] Ogretmen ogretmen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yeniOgretmen = this.ogretmen.OgretmenEkle(ogretmen);
            return CreatedAtAction(nameof(GetOgretmen), new { id = yeniOgretmen.OgretmenId }, yeniOgretmen);
        }

        // PUT: api/Ogretmen/5
        [HttpPut("{id}")]
        public ActionResult<Ogretmen> PutOgretmen(int id, [FromBody] Ogretmen ogretmen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guncellenenOgretmen = this.ogretmen.OgretmenGuncelle(id, ogretmen);

            if (guncellenenOgretmen == null)
            {
                return NotFound($"Öğretmen ID {id} bulunamadı.");
            }

            return Ok(guncellenenOgretmen);
        }

        // DELETE: api/Ogretmen/5
        [HttpDelete("{id}")]
        public ActionResult DeleteOgretmen(int id)
        {
            var silindi = this.ogretmen.OgretmenSil(id);

            if (!silindi)
            {
                return NotFound($"Öğretmen ID {id} bulunamadı.");
            }

            return NoContent();
        }
    }
}
