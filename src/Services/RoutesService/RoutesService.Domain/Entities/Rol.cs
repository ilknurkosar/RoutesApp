using RoutesService.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace RoutesService.Domain.Entities
{
    public class Rol : Entity
    {
        public string? Ad { get; set; }

        [JsonIgnore]
        public ICollection<KullaniciRolleri>? KullaniciRolleri { get; set; }

        [JsonIgnore]
        public ICollection<RolIzinleri>? RolIzinleri { get; set; }
    }
}
