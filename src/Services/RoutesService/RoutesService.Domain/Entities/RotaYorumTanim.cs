using System.ComponentModel.DataAnnotations;

namespace RoutesService.Domain.Entities
{
    public class RotaYorumTanim
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
