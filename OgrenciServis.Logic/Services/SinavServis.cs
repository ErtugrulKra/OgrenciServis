using OgrenciServis.DataAccess;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Services
{
    public class SinavServis : ISinav
    {
        private readonly OkulContext _context;

        public SinavServis(OkulContext context)
        {
            _context = context;
        }

        public IEnumerable<SinavDto> TumSinavlariListele()
        {
            try
            {
                var sonuc = from sinav in _context.Sinavlar
                            join ders in _context.Dersler on sinav.DersId equals ders.DersId
                            join ogrenci in _context.Ogrenciler on sinav.OgrenciId equals ogrenci.OgrenciId
                            join ogretmen in _context.Ogretmenler on sinav.OgretmenId equals ogretmen.OgretmenId
                            select new SinavDto
                            {
                                SinavId = sinav.SinavId,
                                DersId = sinav.DersId,
                                DersAdi = ders.DersAdi,
                                OgrenciId = sinav.OgrenciId,
                                OgrenciAdi = ogrenci.Adi,
                                OgrenciSoyadi = ogrenci.Soyadi,
                                OgretmenId = sinav.OgretmenId,
                                OgretmenAdi = ogretmen.OgretmenAdi,
                                OgretmenSoyadi = ogretmen.OgretmenSoyadi,
                                Not = sinav.Not
                            };

                return sonuc.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SinavDto? SinavGetirById(int id)
        {
            try
            {
                var sonuc = (from sinav in _context.Sinavlar
                             join ders in _context.Dersler on sinav.DersId equals ders.DersId
                             join ogrenci in _context.Ogrenciler on sinav.OgrenciId equals ogrenci.OgrenciId
                             join ogretmen in _context.Ogretmenler on sinav.OgretmenId equals ogretmen.OgretmenId
                             where sinav.SinavId == id
                             select new SinavDto
                             {
                                 SinavId = sinav.SinavId,
                                 DersId = sinav.DersId,
                                 DersAdi = ders.DersAdi,
                                 OgrenciId = sinav.OgrenciId,
                                 OgrenciAdi = ogrenci.Adi,
                                 OgrenciSoyadi = ogrenci.Soyadi,
                                 OgretmenId = sinav.OgretmenId,
                                 OgretmenAdi = ogretmen.OgretmenAdi,
                                 OgretmenSoyadi = ogretmen.OgretmenSoyadi,
                                 Not = sinav.Not
                             }).FirstOrDefault();

                return sonuc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Sinav SinavEkle(Sinav sinav)
        {
            try
            {
                _context.Sinavlar.Add(sinav);
                _context.SaveChanges();
                return sinav;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Sinav? SinavGuncelle(int id, Sinav sinav)
        {
            try
            {
                var mevcutSinav = _context.Sinavlar.Find(id);

                if (mevcutSinav == null)
                {
                    return null;
                }

                mevcutSinav.DersId = sinav.DersId;
                mevcutSinav.OgrenciId = sinav.OgrenciId;
                mevcutSinav.OgretmenId = sinav.OgretmenId;
                mevcutSinav.Not = sinav.Not;

                _context.SaveChanges();
                return mevcutSinav;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SinavSil(int id)
        {
            try
            {
                var sinav = _context.Sinavlar.Find(id);

                if (sinav == null)
                {
                    return false;
                }

                _context.Sinavlar.Remove(sinav);
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

