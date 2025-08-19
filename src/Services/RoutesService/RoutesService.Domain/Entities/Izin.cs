using RoutesService.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace RoutesService.Domain.Entities
{
    public class Izin : Entity
    {
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<RolIzinleri>? RolIzinleri { get; set; }
    }
}
