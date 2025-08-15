using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class Izin: Entity
    {   
        public string? Name { get; set; }

        public ICollection<RolIzinleri>? RolIzinleri { get; set; }
    }
}
