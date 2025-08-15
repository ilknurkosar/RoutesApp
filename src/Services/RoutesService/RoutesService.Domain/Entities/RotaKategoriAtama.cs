using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class RotaKategoriAtama: Entity
    {
        public int RotaId { get; set; }
        public RotaTanim? Rota { get; set; }

        public int KategoriId { get; set; }
        public RotaKategoriTanim? Kategori { get; set; }

    }
}
