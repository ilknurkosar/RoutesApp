namespace RoutesService.Domain.Entities
{
    public class Izin
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<RolIzinleri>? RolIzinleri { get; set; }
    }
}
