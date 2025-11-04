using OgrenciServis.DataAccess;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Services
{
    public class OgrenciServisImpl : Interface.IOgrenci
    {
        private readonly OkulContext _context;

        public OgrenciServisImpl(OkulContext context)
        {
            _context = context;
        }

        public IEnumerable<OgrenciDto> TumOgrencileriListele()
        {
            try
            {
                var sonuc = from ogrenci in _context.Ogrenciler
                            join sinif in _context.Siniflar on ogrenci.SinifId equals sinif.SinifId
                            select new OgrenciDto
                            {
                                OgrenciId = ogrenci.OgrenciId,
                                Adi = ogrenci.Adi,
                                Soyadi = ogrenci.Soyadi,
                                DogumTarihi = ogrenci.DogumTarihi,
                                Sube = sinif.Sube,
                                SinifNo = sinif.SinifNo
                            };

                return sonuc.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
