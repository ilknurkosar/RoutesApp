using RoutesService.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace RoutesService.Domain.Entities
{
    public class RotaKategoriTanim : Entity
    {
        public string? Ad { get; set; }

        [JsonIgnore]
        public ICollection<RotaKategoriAtama> RotaKategoriler { get; set; } = new List<RotaKategoriAtama>();
    }
}
