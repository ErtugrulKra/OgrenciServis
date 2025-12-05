using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciServis.Models
{
    public class User
    {
        [Key]
        [Column(name: "user_id")]
        public int UserId { get; set; }

        [Column(name: "username")]
        public string UserName { get; set; }

        [Column(name: "password")]
        public string Password { get; set; }

        [Column(name: "role")]
        public string Role { get; set; }
    }
}
