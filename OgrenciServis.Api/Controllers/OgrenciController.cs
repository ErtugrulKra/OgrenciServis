using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using OgrenciServis.Models.DTO;
using OgrenciServis.Models.Exceptions;

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
        [AllowAnonymous]
        public ActionResult<OgrenciDto> GetOgrenci(int id)
        {
            try
            {
                var ogrenciDto = this.ogrenci.OgrenciGetirById(id);
                return Ok(ogrenciDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //string concat
                //return StatusCode(500, "Bilinmeyen bir hata olustu " + ex.Message + " " +  id.ToString()); 
                //string interpolation
                return StatusCode(500, $"Bilinmeyen bir hata olustu {ex.Message} {id}");
            }

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
