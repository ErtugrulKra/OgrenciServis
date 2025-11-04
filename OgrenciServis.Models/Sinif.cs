using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciServis.Models
{
   
    public class Sinif
    {
        [Key]
        [Column(name: "sinif_id")]
        public int SinifId { get; set; }

        [Column(name: "sube")]
        public string Sube { get; set; }

        [Column(name: "sinif")]
        public int? SinifNo { get; set; }
    }
}
