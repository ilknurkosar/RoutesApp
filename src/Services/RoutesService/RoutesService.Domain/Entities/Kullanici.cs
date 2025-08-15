using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class Kullanici: Entity
    {
        public string? Ad { get; set; }

        public string? Soyad { get; set; }

        public string? KullaniciAdi { get; set; }

        public string? Email { get; set; }

        public string? Telefon { get; set; }

        public string? Sifre { get; set; }

        [Required]
        [JsonIgnore]
        public string? SifreHash { get; set; }

        public int? RolId { get; set; }
        public Rol? Rol { get; set; }

        public ICollection<KullaniciRolleri>? KullaniciRolleri { get; set; }

        public ICollection<RotaYorumTanim>? Yorumlar { get; set; }
    }
}
