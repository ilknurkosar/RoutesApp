using RoutesService.Domain.Entities.Base;
namespace RoutesService.Domain.Entities
{
    public class KurumTanim: Entity
    {
        public string? Ad { get; set; }

        public string? Telefon { get; set; }

        public string? Email { get; set; }

        public string? WebSitesi { get; set; }

        public string? Adres { get; set; }

        public ICollection<YetkiAlaniTanim>? YetkiAlanlari { get; set; }
    }
}
