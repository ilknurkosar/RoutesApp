using System.ComponentModel.DataAnnotations.Schema;
using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class RotaResimTanim: Entity
    {
        public int? RotaId { get; set; }

        public RotaTanim? Rota { get; set; }

        public string? ResimAdi { get; set; }

        public byte[]? Resim { get; set; }

        public string? ResimUzanti { get; set; }

        public long? ResimBoyut { get; set; }

        public string? AciklamaDetay { get; set; }
    }
}
