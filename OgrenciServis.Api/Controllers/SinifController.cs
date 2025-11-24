using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinifController : ControllerBase
    {
        private readonly ISinif sinif;

        public SinifController(ISinif sinif)
        {
            this.sinif = sinif;
        }

        // GET: api/Sinif
        [HttpGet]
        public ActionResult<IEnumerable<SinifDto>> GetSiniflar()
        {
            return Ok(this.sinif.TumSiniflariListele());
        }

        // GET: api/Sinif/5
        [HttpGet("{id}")]
        public ActionResult<SinifDto> GetSinif(int id)
        {
            var sinifDto = this.sinif.SinifGetirById(id);

            if (sinifDto == null)
            {
                return NotFound($"Sınıf ID {id} bulunamadı.");
            }

            return Ok(sinifDto);
        }

        // POST: api/Sinif
        [HttpPost]
        public ActionResult<Sinif> PostSinif([FromBody] Sinif sinif)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yeniSinif = this.sinif.SinifEkle(sinif);
            return CreatedAtAction(nameof(GetSinif), new { id = yeniSinif.SinifId }, yeniSinif);
        }

        // PUT: api/Sinif/5
        [HttpPut("{id}")]
        public ActionResult<Sinif> PutSinif(int id, [FromBody] Sinif sinif)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guncellenenSinif = this.sinif.SinifGuncelle(id, sinif);

            if (guncellenenSinif == null)
            {
                return NotFound($"Sınıf ID {id} bulunamadı.");
            }

            return Ok(guncellenenSinif);
        }

        // DELETE: api/Sinif/5
        [HttpDelete("{id}")]
        public ActionResult DeleteSinif(int id)
        {
            var silindi = this.sinif.SinifSil(id);

            if (!silindi)
            {
                return NotFound($"Sınıf ID {id} bulunamadı.");
            }

            return NoContent();
        }
    }
}

