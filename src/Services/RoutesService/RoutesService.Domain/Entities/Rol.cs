using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class Rol : Entity
    {
        public string? Ad { get; set; }

        public ICollection<Kullanici>? Kullanicilar { get; set; }
       
        public Rol() { 
         Kullanicilar = new List<Kullanici>();
        }
    }
}
