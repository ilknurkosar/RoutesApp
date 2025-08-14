using RoutesService.Domain.Entities.Base;

namespace RoutesService.Domain.Entities
{
    public class Rol : Entity
    {
        public string? Ad { get; set; }

        public ICollection<Kullanici>? Kullanicilar { get; set; }
       
        public bool? RotaEklemeYetkisiVarMi { get; set; }
        
        public bool? RotaGuncellemeYetkisiVarMi { get; set; }

        public bool? RotaSilmeYetkisiVarMi { get; set; }

        public bool? RolDuzenlemeYetkisiVarMi{ get; set; }

        public bool? RolSilmeYetkisiVarMi { get; set; }

        public bool? RotaYorumSilmeYetkisiVarMi { get; set; }

        public bool? RotaYorumEklemeYetkisiVarMi { get; set; }

        public bool? RotaYorumGuncellemeYetkisiVarMi { get; set; }

        public bool? RotaKategoriGuncellemeYetkisiVarMi { get; set; }

        public bool? RotaKategoriSilmeYetkisiVarMi { get; set; }

        public bool? YetkiAlaniEklemeYetkisiVarMi { get; set; }

        public bool? YetkiAlaniGuncellemeYetkisiVarMi { get; set; }

        public bool? YetkiAlaniSilmeYetkisiVarMi { get; set; }

        public bool? YetkiAlaniGoruntulemeYetkisiVarMi { get; set; }

        public bool? KurumEklemeYetkisiVarMi { get; set; }

        public bool? KurumGuncellemeYetkisiVarMi { get; set; }

        public bool? KurumSilmeYetkisiVarMi { get; set; }

        public Rol() { 
         Kullanicilar = new List<Kullanici>();
        }
    }
}
