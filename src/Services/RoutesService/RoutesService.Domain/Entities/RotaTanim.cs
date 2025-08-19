using RoutesService.Domain.Entities.Base;
using NetTopologySuite.Geometries;
using System.Text.Json.Serialization;

namespace RoutesService.Domain.Entities
{
    public class RotaTanim : Entity
    {
        public string? Adi { get; set; }
        public string? Renk { get; set; }
        public Geometry? Geometry { get; set; }
        public double? Mesafe { get; set; }
        public int? TahminiSureDakika { get; set; }

        [JsonIgnore]
        public ICollection<RotaKategoriAtama> RotaKategoriler { get; set; } = new List<RotaKategoriAtama>();

        [JsonIgnore]
        public ICollection<RotaResimTanim>? Resimler { get; set; }

        [JsonIgnore]
        public ICollection<RotaYorumTanim>? Yorumlar { get; set; }

        [JsonIgnore]
        public ICollection<RotaOnemliYerTanim>? OnemliYerler { get; set; }
    }
}
