using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciServis.Models
{
    public class Ders
    {
        [Key]
        [Column(name: "ders_id")]
        public int DersId { get; set; }

        [Column(name: "ders_adi")]
        public string DersAdi { get; set; }

        [Column(name: "ders_suresi")]
        public int? DersSuresi { get; set; }
    }
}
