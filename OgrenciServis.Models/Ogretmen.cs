using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciServis.Models
{
    
    public class Ogretmen
    {
        [Key]
        [Column(name: "ogretmen_id")]
        public int OgretmenId { get; set; }

        [Column(name: "ogretmen_adi")]
        public string OgretmenAdi { get; set; }

        [Column(name: "ogretmen_soyadi")]
        public string OgretmenSoyadi { get; set; }

        [Column(name: "brans")]
        public string Brans { get; set; }

        [Column(name: "sinif")]
        public int? Sinif { get; set; }
    }
}
