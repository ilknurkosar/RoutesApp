namespace RoutesService.Domain.Entities.Base
{
    public abstract class Entity
    {
        public int Id { get; set; } = default;

        public string? Aciklama { get; set; }

        public bool? AktifMi { get; set; }

        public bool? SilindiMi { get; set; }

        public string? EkleyenKullaniciId { get; set; }

        public DateTime? EklenmeTarihi { get; set; }

        public string? GuncelleyenKullaniciId { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public string? SilenKullaniciId { get; set; }

        public DateTime? SilinmeTarihi { get; set; }
    }
}
