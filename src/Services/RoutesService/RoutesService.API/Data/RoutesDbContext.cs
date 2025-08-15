using Microsoft.EntityFrameworkCore;
using RoutesService.Domain.Entities;
using RoutesService.Domain.Entities.Base;

namespace RoutesService.API.Data
{
    public class RoutesDbContext : DbContext
    {

        public RoutesDbContext(DbContextOptions<RoutesDbContext> options) : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }

        public DbSet<Rol> Roller { get; set; }

        public DbSet<Izin> Izinler { get; set; }

        public DbSet<KullaniciRolleri> KullaniciRolleri { get; set; }

        public DbSet<RotaTanim> Rotalar { get; set; }

        public DbSet<RolIzinleri> RolIzinleri { get; set; }

        public DbSet<RotaKategoriTanim> RotaKategoriler { get; set; }

        public DbSet<RotaOnemliYerTanim> RotaOnemliYerler { get; set; }

        public DbSet<RotaResimTanim> RotaResimler { get; set; }

        public DbSet<RotaYorumTanim> RotaYorumlar { get; set; }

        public DbSet<KurumTanim> Kurumlar { get; set; }

        public DbSet<YetkiAlaniTanim> YetkiAlanlari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //Base Entity için ortak yapı
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            { 

                if(typeof(Entity).IsAssignableFrom(entityType.ClrType)) //Sadece Entity classından türeyen entityler

                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>("EklenmeTarihi")
                        .HasDefaultValueSql("NOW()");
                }
            }

            modelBuilder.Entity<Kullanici>()
                .HasOne(k => k.Rol)
                .WithMany(r => r.Kullanicilar)
                .HasForeignKey(k => k.RolId);

            modelBuilder.Entity<KullaniciRolleri>()
               .HasKey(kr => new { kr.KullaniciId, kr.RolId }); // Composite key

            modelBuilder.Entity<KullaniciRolleri>()
                .HasOne(kr => kr.Kullanici)
                .WithMany(k => k.KullaniciRolleri)
                .HasForeignKey(kr => kr.KullaniciId);

            modelBuilder.Entity<KullaniciRolleri>()
                .HasOne(kr => kr.Rol)
                .WithMany(r => r.KullaniciRolleri)
                .HasForeignKey(kr => kr.RolId);

            modelBuilder.Entity<RolIzinleri>()
               .HasKey(ri => new { ri.RolId, ri.IzinId }); // Composite key

            modelBuilder.Entity<RolIzinleri>()
                .HasOne(ri => ri.Rol)
                .WithMany(r => r.RolIzinleri)
                .HasForeignKey(ri => ri.RolId);

            modelBuilder.Entity<RolIzinleri>()
                .HasOne(ri => ri.Izin)
                .WithMany(i => i.RolIzinleri)
                .HasForeignKey(ri => ri.IzinId);

            //many to many olmalı mı?
            modelBuilder.Entity<RotaTanim>()
                .HasOne(rt => rt.Kategori)
                .WithMany(k => k.Rotalar)
                .HasForeignKey(rt => rt.KategoriId);
            
           modelBuilder.Entity<RotaOnemliYerTanim>()
                .HasOne(r => r.Rota)
                .WithMany(k => k.OnemliYerler)
                .HasForeignKey(r => r.RotaId)
                .OnDelete(DeleteBehavior.Cascade); // Rota silindiğinde ilgili önemli yerler de silinsin

            modelBuilder.Entity<RotaResimTanim>()
                .HasOne(r => r.Rota)
                .WithMany(k => k.Resimler)
                .HasForeignKey(r => r.RotaId)
                .OnDelete(DeleteBehavior.Cascade); // Rota silindiğinde ilgili resimler de silinsin

            modelBuilder.Entity<RotaYorumTanim>()
                .HasOne(r=> r.Rota)
                .WithMany(k => k.Yorumlar)
                .HasForeignKey(r => r.RotaId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<YetkiAlaniTanim>()
                .HasOne(r=>r.Kurum)
                .WithMany(k => k.YetkiAlanlari)
                .HasForeignKey(r => r.KurumId)
                .OnDelete(DeleteBehavior.Cascade); 

        }

    }

}
