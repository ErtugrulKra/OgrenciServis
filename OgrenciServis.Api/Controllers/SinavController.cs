using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinavController : ControllerBase
    {
        private readonly ISinav sinav;

        public SinavController(ISinav sinav)
        {
            this.sinav = sinav;
        }

        // GET: api/Sinav
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<SinavDto>> GetSinavlar()
        {
            return Ok(this.sinav.TumSinavlariListele());
        }

        // GET: api/Sinav/5
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public ActionResult<SinavDto> GetSinav(int id)
        {
            var sinavDto = this.sinav.SinavGetirById(id);

            if (sinavDto == null)
            {
                return NotFound($"Sınav ID {id} bulunamadı.");
            }

            return Ok(sinavDto);
        }

        // POST: api/Sinav
        [HttpPost]
        public ActionResult<Sinav> PostSinav([FromBody] Sinav sinav)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yeniSinav = this.sinav.SinavEkle(sinav);
            return CreatedAtAction(nameof(GetSinav), new { id = yeniSinav.SinavId }, yeniSinav);
        }

        // PUT: api/Sinav/5
        [HttpPut("{id}")]
        public ActionResult<Sinav> PutSinav(int id, [FromBody] Sinav sinav)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guncellenenSinav = this.sinav.SinavGuncelle(id, sinav);

            if (guncellenenSinav == null)
            {
                return NotFound($"Sınav ID {id} bulunamadı.");
            }

            return Ok(guncellenenSinav);
        }

        // DELETE: api/Sinav/5
        [HttpDelete("{id}")]
        public ActionResult DeleteSinav(int id)
        {
            var silindi = this.sinav.SinavSil(id);

            if (!silindi)
            {
                return NotFound($"Sınav ID {id} bulunamadı.");
            }

            return NoContent();
        }
    }
}

