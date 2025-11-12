using OgrenciServis.DataAccess;
using OgrenciServis.Models;
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

        public OgrenciDto? OgrenciGetirById(int id)
        {
            try
            {
                var sonuc = (from ogrenci in _context.Ogrenciler
                             join sinif in _context.Siniflar on ogrenci.SinifId equals sinif.SinifId
                             where ogrenci.OgrenciId == id
                             select new OgrenciDto
                             {
                                 OgrenciId = ogrenci.OgrenciId,
                                 Adi = ogrenci.Adi,
                                 Soyadi = ogrenci.Soyadi,
                                 DogumTarihi = ogrenci.DogumTarihi,
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

        public Ogrenci OgrenciEkle(Ogrenci ogrenci)
        {
            try
            {
                _context.Ogrenciler.Add(ogrenci);
                _context.SaveChanges();
                return ogrenci;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Ogrenci? OgrenciGuncelle(int id, Ogrenci ogrenci)
        {
            try
            {
                var mevcutOgrenci = _context.Ogrenciler.Find(id);

                if (mevcutOgrenci == null)
                {
                    return null;
                }

                mevcutOgrenci.Adi = ogrenci.Adi;
                mevcutOgrenci.Soyadi = ogrenci.Soyadi;
                mevcutOgrenci.DogumTarihi = ogrenci.DogumTarihi;
                mevcutOgrenci.SinifId = ogrenci.SinifId;

                _context.SaveChanges();
                return mevcutOgrenci;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool OgrenciSil(int id)
        {
            try
            {
                var ogrenci = _context.Ogrenciler.Find(id);

                if (ogrenci == null)
                {
                    return false;
                }

                _context.Ogrenciler.Remove(ogrenci);
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
