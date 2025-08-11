using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class RotaKategoriTanim:Entity
    {
        public string? Ad { get; set; }

        public ICollection<RotaTanim>? Rotalar { get; set; }
    }
}
