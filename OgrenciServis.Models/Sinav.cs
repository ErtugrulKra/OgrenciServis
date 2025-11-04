using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciServis.Models
{

    public class Sinav
    {
        [Key]
        [Column(name: "sinav_id")]
        public int SinavId { get; set; }

        [Column(name: "ders_id")]
        public int? DersId { get; set; }

        [Column(name: "ogrenci_id")]
        public int? OgrenciId { get; set; }

        [Column(name: "ogretmen_id")]
        public int? OgretmenId { get; set; }

        [Column(name: "not")]
        public int? Not { get; set; }
    }
}
