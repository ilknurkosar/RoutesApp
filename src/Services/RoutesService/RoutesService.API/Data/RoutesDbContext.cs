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

        public DbSet<RotaTanim> Rotalar { get; set; }

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
