using OgrenciServis.Models;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Interface
{
    public interface IOgretmen
    {
        IEnumerable<OgretmenDto> TumOgretmenleriListele();
        
        OgretmenDto? OgretmenGetirById(int id);
        
        Ogretmen OgretmenEkle(Ogretmen ogretmen);
        
        Ogretmen? OgretmenGuncelle(int id, Ogretmen ogretmen);
        
        bool OgretmenSil(int id);
    }
}