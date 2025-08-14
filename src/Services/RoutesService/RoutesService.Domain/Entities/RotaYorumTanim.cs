using System.ComponentModel.DataAnnotations;
using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class RotaYorumTanim: Entity
    {
        public int? RotaId { get; set; }

        public RotaTanim? Rota { get; set; }

        public int? KullaniciId { get; set; }

        public Kullanici? Kullanici { get; set; }

        [Range(1, 5)]
        public int? Puan { get; set; }

        [MaxLength(500)]
        public string? Yorum { get; set; }
    }
}
