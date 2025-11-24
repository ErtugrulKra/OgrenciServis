using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Interface
{
    public interface ISinif
    {
        IEnumerable<SinifDto> TumSiniflariListele();

        SinifDto? SinifGetirById(int id);

        Sinif SinifEkle(Sinif sinif);

        Sinif? SinifGuncelle(int id, Sinif sinif);

        bool SinifSil(int id);
    }
}

