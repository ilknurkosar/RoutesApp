namespace RoutesService.Domain.Entities
{
    public class KullaniciRolleri
    {
        public int KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

        public int RolId { get; set; }
        public Rol? Rol { get; set; }
    }
}
