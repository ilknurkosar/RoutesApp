namespace RoutesService.Domain.Entities
{
    public class Izin
    {   
        public string? Name { get; set; }

        public ICollection<RolIzinleri>? RolIzinleri { get; set; }
    }
}
