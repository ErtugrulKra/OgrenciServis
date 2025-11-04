using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciServis.Models
{
    public class Ogrenci
    {
        [Key]
        [Column(name:"ogrenci_id")]
        public int OgrenciId { get; set; }

        [Column(name:"adi")] //Mapping
        public string Adi { get; set; }

        [Column(name:"soyadi")]
        public string Soyadi { get; set; }

        [Column(name:"dogum_tarihi")]
        public DateTime? DogumTarihi { get; set; }

        [Column(name:"sinif_id")]
        public int? SinifId { get; set; }
    }
}
