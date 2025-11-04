using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Interface
{
    public interface IOgretmen
    {
        IEnumerable<OgretmenDto> TumOgretmenleriListele();
    }
} 