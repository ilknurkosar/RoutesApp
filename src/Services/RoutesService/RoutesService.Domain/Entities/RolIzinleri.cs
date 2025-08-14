namespace RoutesService.Domain.Entities
{
    public class RolIzinleri
    {
        public int RolId { get; set; }
        public Rol? Rol { get; set; }

        public int IzinId { get; set; }
        public Izin? Izin { get; set; }

       }
}
