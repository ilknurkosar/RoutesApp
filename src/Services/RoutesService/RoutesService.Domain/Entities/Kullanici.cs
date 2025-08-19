using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class Kullanici : Entity
    {
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? KullaniciAdi { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public string? Sifre { get; set; }


        [JsonIgnore]
        public ICollection<KullaniciRolleri>? KullaniciRolleri { get; set; }

        [JsonIgnore]
        public ICollection<RotaYorumTanim>? Yorumlar { get; set; }
    }
}
