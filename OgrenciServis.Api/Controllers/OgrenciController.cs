using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
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

        // GET: api/Ogrenci
        [HttpGet]
        public ActionResult<IEnumerable<OgrenciDto>> GetOgrenciler()
        {
            return Ok(this.ogrenci.TumOgrencileriListele());
        }

        // GET: api/Ogrenci/5
        [HttpGet("{id}")]
        public ActionResult<OgrenciDto> GetOgrenci(int id)
        {
            var ogrenciDto = this.ogrenci.OgrenciGetirById(id);

            if (ogrenciDto == null)
            {
                return NotFound($"Öğrenci ID {id} bulunamadı.");
            }

            return Ok(ogrenciDto);
        }

        // POST: api/Ogrenci
        [HttpPost]
        public ActionResult<Ogrenci> PostOgrenci([FromBody] Ogrenci ogrenci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yeniOgrenci = this.ogrenci.OgrenciEkle(ogrenci);
            return CreatedAtAction(nameof(GetOgrenci), new { id = yeniOgrenci.OgrenciId }, yeniOgrenci);
        }

        // PUT: api/Ogrenci/5
        [HttpPut("{id}")]
        public ActionResult<Ogrenci> PutOgrenci(int id, [FromBody] Ogrenci ogrenci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guncellenenOgrenci = this.ogrenci.OgrenciGuncelle(id, ogrenci);

            if (guncellenenOgrenci == null)
            {
                return NotFound($"Öğrenci ID {id} bulunamadı.");
            }

            return Ok(guncellenenOgrenci);
        }

        // DELETE: api/Ogrenci/5
        [HttpDelete("{id}")]
        public ActionResult DeleteOgrenci(int id)
        {
            var silindi = this.ogrenci.OgrenciSil(id);

            if (!silindi)
            {
                return NotFound($"Öğrenci ID {id} bulunamadı.");
            }

            return NoContent();
        }
    }
}
