using RoutesService.Domain.Entities.Base;
using NetTopologySuite.Geometries;

namespace RoutesService.Domain.Entities
{
    public class RotaTanim : Entity
    {
        public string? Adi { get; set; }

        public string? Renk { get; set; }

        public Geometry? Geometry { get; set; }

        public double? Mesafe{ get; set; }

        public int? TahminiSureDakika{ get; set; }

        public ICollection<RotaKategoriAtama> RotaKategoriler { get; set; } = new List<RotaKategoriAtama>();

        public ICollection<RotaResimTanim>? Resimler { get; set; }

        public ICollection<RotaYorumTanim>? Yorumlar { get; set; }

        public ICollection<RotaOnemliYerTanim>? OnemliYerler { get; set; }
    }
}
