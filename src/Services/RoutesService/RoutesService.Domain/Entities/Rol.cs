using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class Rol : Entity
    {
        public string? Ad { get; set; }

        public ICollection<KullaniciRolleri>? KullaniciRolleri { get; set; }
        
        public ICollection<RolIzinleri>? RolIzinleri { get; set; }

    }
}
