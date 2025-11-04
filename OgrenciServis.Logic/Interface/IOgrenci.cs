using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Interface
{
    public interface IOgrenci
    {
        IEnumerable<OgrenciDto> TumOgrencileriListele();
    }
}
