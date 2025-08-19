using NetTopologySuite.Geometries;
using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class YetkiAlaniTanim: Entity
    {
        public string? Ad { get; set; }

        public string? Renk { get; set; }

        public Geometry? Geometry { get; set; }

        public int? KurumId { get; set; }

        public KurumTanim? Kurum { get; set; }
    }
}
