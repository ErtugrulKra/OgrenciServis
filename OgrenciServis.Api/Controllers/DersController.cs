using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DersController : ControllerBase
    {
        private readonly IDers ders;

        public DersController(IDers ders)
        {
            this.ders = ders;
        }

        // GET: api/Ders
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<DersDto>> GetDersler()
        {
            return Ok(this.ders.TumDersleriListele());
        }

        // GET: api/Ders/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<DersDto> GetDers(int id)
        {
            var dersDto = this.ders.DersGetirById(id);

            if (dersDto == null)
            {
                return NotFound($"Ders ID {id} bulunamadı.");
            }

            return Ok(dersDto);
        }

        // POST: api/Ders
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Ders> PostDers([FromBody] Ders ders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yeniDers = this.ders.DersEkle(ders);
            return CreatedAtAction(nameof(GetDers), new { id = yeniDers.DersId }, yeniDers);
        }

        // PUT: api/Ders/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Ders> PutDers(int id, [FromBody] Ders ders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guncellenenDers = this.ders.DersGuncelle(id, ders);

            if (guncellenenDers == null)
            {
                return NotFound($"Ders ID {id} bulunamadı.");
            }

            return Ok(guncellenenDers);
        }

        // DELETE: api/Ders/5
        [HttpDelete("{id}")]
        public ActionResult DeleteDers(int id)
        {
            var silindi = this.ders.DersSil(id);

            if (!silindi)
            {
                return NotFound($"Ders ID {id} bulunamadı.");
            }

            return NoContent();
        }
    }
}

