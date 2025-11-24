using OgrenciServis.DataAccess;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Services
{
    public class SinifServis : ISinif
    {
        private readonly OkulContext _context;

        public SinifServis(OkulContext context)
        {
            _context = context;
        }

        public IEnumerable<SinifDto> TumSiniflariListele()
        {
            try
            {
                var sonuc = from sinif in _context.Siniflar
                            select new SinifDto
                            {
                                SinifId = sinif.SinifId,
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

        public SinifDto? SinifGetirById(int id)
        {
            try
            {
                var sonuc = (from sinif in _context.Siniflar
                             where sinif.SinifId == id
                             select new SinifDto
                             {
                                 SinifId = sinif.SinifId,
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

        public Sinif SinifEkle(Sinif sinif)
        {
            try
            {
                _context.Siniflar.Add(sinif);
                _context.SaveChanges();
                return sinif;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Sinif? SinifGuncelle(int id, Sinif sinif)
        {
            try
            {
                var mevcutSinif = _context.Siniflar.Find(id);

                if (mevcutSinif == null)
                {
                    return null;
                }

                mevcutSinif.Sube = sinif.Sube;
                mevcutSinif.SinifNo = sinif.SinifNo;

                _context.SaveChanges();
                return mevcutSinif;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SinifSil(int id)
        {
            try
            {
                var sinif = _context.Siniflar.Find(id);

                if (sinif == null)
                {
                    return false;
                }

                _context.Siniflar.Remove(sinif);
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

