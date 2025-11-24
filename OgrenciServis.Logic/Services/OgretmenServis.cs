using OgrenciServis.DataAccess;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Services
{
    public class OgretmenServis : IOgretmen
    {
        private readonly OkulContext _context;

        public OgretmenServis(OkulContext context)
        {
            _context = context;
        }
        public IEnumerable<OgretmenDto> TumOgretmenleriListele()
        {
            try
            {
                //Linq - Lang Integrated Query
                var sonuc = from ogretmen in _context.Ogretmenler
                            join sinif in _context.Siniflar on ogretmen.Sinif equals sinif.SinifId
                            select new OgretmenDto
                            {
                                OgretmenId = ogretmen.OgretmenId,
                                Adi = ogretmen.OgretmenAdi,
                                Soyadi = ogretmen.OgretmenSoyadi,
                                Brans = ogretmen.Brans,
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

        public OgretmenDto? OgretmenGetirById(int id)
        {
            try
            {
                var sonuc = (from ogretmen in _context.Ogretmenler
                             join sinif in _context.Siniflar on ogretmen.Sinif equals sinif.SinifId
                             where ogretmen.OgretmenId == id
                             select new OgretmenDto
                             {
                                 OgretmenId = ogretmen.OgretmenId,
                                 Adi = ogretmen.OgretmenAdi,
                                 Soyadi = ogretmen.OgretmenSoyadi,
                                 Brans = ogretmen.Brans,
                                 Sube = sinif.Sube,
                                 SinifNo = sinif.SinifNo
                             }).FirstOrDefault();

                return sonuc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Ogretmen OgretmenEkle(Ogretmen ogretmen)
        {
            try
            {
                _context.Ogretmenler.Add(ogretmen);
                _context.SaveChanges();
                return ogretmen;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Ogretmen? OgretmenGuncelle(int id, Ogretmen ogretmen)
        {
            try
            {
                var mevcutOgretmen = _context.Ogretmenler.Find(id);

                if (mevcutOgretmen == null)
                {
                    return null;
                }

                mevcutOgretmen.OgretmenAdi = ogretmen.OgretmenAdi;
                mevcutOgretmen.OgretmenSoyadi = ogretmen.OgretmenSoyadi;
                mevcutOgretmen.Brans = ogretmen.Brans;
                mevcutOgretmen.Sinif = ogretmen.Sinif;

                _context.SaveChanges();
                return mevcutOgretmen;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool OgretmenSil(int id)
        {
            try
            {
                var ogretmen = _context.Ogretmenler.Find(id);

                if (ogretmen == null)
                {
                    return false;
                }

                _context.Ogretmenler.Remove(ogretmen);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
