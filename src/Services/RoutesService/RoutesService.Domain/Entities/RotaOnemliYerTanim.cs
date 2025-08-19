using NetTopologySuite.Geometries;
using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class RotaOnemliYerTanim : Entity
    {
        public int? RotaId { get; set; }

        public RotaTanim? Rota { get; set; }

        public string? Ad { get; set; }

        public string? AciklamaDetay { get; set; }

        public Geometry? Geometry { get; set; }
    }
}
