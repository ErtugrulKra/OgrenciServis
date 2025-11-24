using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Interface
{
    public interface IDers
    {
        IEnumerable<DersDto> TumDersleriListele();

        DersDto? DersGetirById(int id);

        Ders DersEkle(Ders ders);

        Ders? DersGuncelle(int id, Ders ders);

        bool DersSil(int id);
    }
}

