using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Interface
{
    public interface IOgrenci
    {
        IEnumerable<OgrenciDto> TumOgrencileriListele();

        OgrenciDto? OgrenciGetirById(int id);
        
        Ogrenci OgrenciEkle(Ogrenci ogrenci);
        
        Ogrenci? OgrenciGuncelle(int id, Ogrenci ogrenci);
        
        bool OgrenciSil(int id);
    }
}
