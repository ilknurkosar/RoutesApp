using Microsoft.EntityFrameworkCore;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(RoutesDbContext context)
        {
            // Tablo varlığını kontrol et
            if (!await context.Database.CanConnectAsync())
            {
                throw new InvalidOperationException("Veritabanına bağlanılamıyor!");
            }

            // Ana entity'leri önce oluştur (ilişkisiz)
            if (!context.Kullanicilar.Any())
            {
                await SeedKullanicilarAsync(context);
            }

            if (!context.Roller.Any())
            {
                await SeedRollerAsync(context);
            }

            if (!context.Izinler.Any())
            {
                await SeedIzinlerAsync(context);
            }

            if (!context.Kurumlar.Any())
            {
                await SeedKurumlarAsync(context);
            }

            if (!context.RotaKategoriler.Any())
            {
                await SeedRotaKategorilerAsync(context);
            }

            if (!context.Rotalar.Any())
            {
                await SeedRotalarAsync(context);
            }

            // İlişkili entity'leri sonra oluştur
            if (!context.YetkiAlanlari.Any())
            {
                await SeedYetkiAlanlariAsync(context);
            }

            if (!context.KullaniciRolleri.Any())
            {
                await SeedKullaniciRolleriAsync(context);
            }

            if (!context.RolIzinleri.Any())
            {
                await SeedRolIzinleriAsync(context);
            }

            if (!context.RotaKategoriAtama.Any())
            {
                await SeedRotaKategoriAtamaAsync(context);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedKullanicilarAsync(RoutesDbContext context)
        {
            var kullanicilar = new List<Kullanici>
            {
                new Kullanici
                {
                    Id = 1,
                    Ad = "Admin",
                    Soyad = "Kullanıcı",
                    Email = "admin@routesapp.com",
                    KullaniciAdi = "admin",
                                       
                    Telefon = "5551234567",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new Kullanici
                {
                    Id = 2,
                    Ad = "Test",
                    Soyad = "Kullanıcı",
                    Email = "test@routesapp.com",
                    KullaniciAdi = "test",
                                       
                    Telefon = "5559876543",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.Kullanicilar.AddRangeAsync(kullanicilar);
        }

        private static async Task SeedRollerAsync(RoutesDbContext context)
        {
            var roller = new List<Rol>
            {
                new Rol
                {
                    Id = 1,
                    Ad = "Admin",
                    Aciklama = "Sistem yöneticisi",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new Rol
                {
                    Id = 2,
                    Ad = "Kullanıcı",
                    Aciklama = "Normal kullanıcı",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new Rol
                {
                    Id = 3,
                    Ad = "Moderatör",
                    Aciklama = "İçerik moderatörü",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.Roller.AddRangeAsync(roller);
        }

        private static async Task SeedIzinlerAsync(RoutesDbContext context)
        {
            var izinler = new List<Izin>
            {
                new Izin
                {
                    Id = 1,
                    Name = "Kullanıcı Yönetimi",
                    Aciklama = "Kullanıcı ekleme, düzenleme, silme",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new Izin
                {
                    Id = 2,
                    Name = "Rol Yönetimi",
                    Aciklama = "Rol ekleme, düzenleme, silme",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new Izin
                {
                    Id = 3,
                    Name = "Rota Görüntüleme",
                    Aciklama = "Rotaları görüntüleme",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new Izin
                {
                    Id = 4,
                    Name = "Rota Ekleme",
                    Aciklama = "Yeni rota ekleme",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new Izin
                {
                    Id = 5,
                    Name = "Rota Düzenleme",
                    Aciklama = "Mevcut rotaları düzenleme",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.Izinler.AddRangeAsync(izinler);
        }

        private static async Task SeedKurumlarAsync(RoutesDbContext context)
        {
            var kurumlar = new List<KurumTanim>
            {
                new KurumTanim
                {
                    Id = 1,
                    Ad = "Genel Kurum",
                    Aciklama = "Varsayılan kurum",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.Kurumlar.AddRangeAsync(kurumlar);
        }

        private static async Task SeedYetkiAlanlariAsync(RoutesDbContext context)
        {
            // Önce kurumu al
            var genelKurum = await context.Kurumlar.FindAsync(1);

            var yetkiAlanlari = new List<YetkiAlaniTanim>
            {
                new YetkiAlaniTanim
                {
                    Id = 1,
                    Ad = "Genel Yetki Alanı",
                    Aciklama = "Tüm sistem için genel yetki alanı",
                    KurumId = 1,
                    Kurum = genelKurum,
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.YetkiAlanlari.AddRangeAsync(yetkiAlanlari);

            // Navigation property'leri güncelle
            if (genelKurum != null)
            {
                genelKurum.YetkiAlanlari = yetkiAlanlari;
            }
        }

        private static async Task SeedRotaKategorilerAsync(RoutesDbContext context)
        {
            var kategoriler = new List<RotaKategoriTanim>
            {
                new RotaKategoriTanim
                {
                    Id = 1,
                    Ad = "Doğa Yürüyüşü",
                    Aciklama = "Doğal ortamlarda yapılan yürüyüş rotaları",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new RotaKategoriTanim
                {
                    Id = 2,
                    Ad = "Şehir Turu",
                    Aciklama = "Şehir içi turistik rotalar",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new RotaKategoriTanim
                {
                    Id = 3,
                    Ad = "Bisiklet Rotası",
                    Aciklama = "Bisiklet ile gidilebilen rotalar",
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.RotaKategoriler.AddRangeAsync(kategoriler);
        }

        private static async Task SeedRotalarAsync(RoutesDbContext context)
        {
            var rotalar = new List<RotaTanim>
            {
                new RotaTanim
                {
                    Id = 1,
                    Adi = "Belgrad Ormanı Yürüyüş Rotası",
                    Aciklama = "İstanbul Belgrad Ormanı'nda doğa ile iç içe yürüyüş rotası",
                    Mesafe = 5.2,
                    TahminiSureDakika = 120,
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new RotaTanim
                {
                    Id = 2,
                    Adi = "Sultanahmet Tarihi Yarımada Turu",
                    Aciklama = "İstanbul'un tarihi yarımadasında kültürel miras turu",
                    Mesafe = 3.8,
                    TahminiSureDakika = 180,
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.Rotalar.AddRangeAsync(rotalar);
        }

        private static async Task SeedKullaniciRolleriAsync(RoutesDbContext context)
        {
            // Önce kullanıcıları ve rolleri al
            var adminKullanici = await context.Kullanicilar.FindAsync(1);
            var testKullanici = await context.Kullanicilar.FindAsync(2);
            var adminRol = await context.Roller.FindAsync(1);
            var kullaniciRol = await context.Roller.FindAsync(2);

            var kullaniciRolleri = new List<KullaniciRolleri>
            {
                new KullaniciRolleri
                {
                    KullaniciId = 1,
                    RolId = 1,
                    Kullanici = adminKullanici,
                    Rol = adminRol,
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new KullaniciRolleri
                {
                    KullaniciId = 2,
                    RolId = 2,
                    Kullanici = testKullanici,
                    Rol = kullaniciRol,
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.KullaniciRolleri.AddRangeAsync(kullaniciRolleri);

            // Navigation property'leri güncelle
            if (adminKullanici != null)
            {
                adminKullanici.KullaniciRolleri = new List<KullaniciRolleri> { kullaniciRolleri[0] };
            }
            if (testKullanici != null)
            {
                testKullanici.KullaniciRolleri = new List<KullaniciRolleri> { kullaniciRolleri[1] };
            }
            if (adminRol != null)
            {
                adminRol.KullaniciRolleri = new List<KullaniciRolleri> { kullaniciRolleri[0] };
            }
            if (kullaniciRol != null)
            {
                kullaniciRol.KullaniciRolleri = new List<KullaniciRolleri> { kullaniciRolleri[1] };
            }
        }

        private static async Task SeedRolIzinleriAsync(RoutesDbContext context)
        {
            // Önce rolleri ve izinleri al
            var adminRol = await context.Roller.FindAsync(1);
            var kullaniciRol = await context.Roller.FindAsync(2);
            var moderatörRol = await context.Roller.FindAsync(3);

            var kullaniciYonetimiIzin = await context.Izinler.FindAsync(1);
            var rolYonetimiIzin = await context.Izinler.FindAsync(2);
            var rotaGoruntulemeIzin = await context.Izinler.FindAsync(3);
            var rotaEklemeIzin = await context.Izinler.FindAsync(4);
            var rotaDuzenlemeIzin = await context.Izinler.FindAsync(5);

            var rolIzinleri = new List<RolIzinleri>
            {
                // Admin rolü tüm izinlere sahip
                new RolIzinleri {
                    RolId = 1, IzinId = 1,
                    Rol = adminRol, Izin = kullaniciYonetimiIzin,
                    AktifMi = true, SilindiMi = false, EkleyenKullaniciId = "system", EklenmeTarihi = DateTime.UtcNow
                },
                new RolIzinleri {
                    RolId = 1, IzinId = 2,
                    Rol = adminRol, Izin = rolYonetimiIzin,
                    AktifMi = true, SilindiMi = false, EkleyenKullaniciId = "system", EklenmeTarihi = DateTime.UtcNow
                },
                new RolIzinleri {
                    RolId = 1, IzinId = 3,
                    Rol = adminRol, Izin = rotaGoruntulemeIzin,
                    AktifMi = true, SilindiMi = false, EkleyenKullaniciId = "system", EklenmeTarihi = DateTime.UtcNow
                },
                new RolIzinleri {
                    RolId = 1, IzinId = 4,
                    Rol = adminRol, Izin = rotaEklemeIzin,
                    AktifMi = true, SilindiMi = false, EkleyenKullaniciId = "system", EklenmeTarihi = DateTime.UtcNow
                },
                new RolIzinleri {
                    RolId = 1, IzinId = 5,
                    Rol = adminRol, Izin = rotaDuzenlemeIzin,
                    AktifMi = true, SilindiMi = false, EkleyenKullaniciId = "system", EklenmeTarihi = DateTime.UtcNow
                },

                // Kullanıcı rolü sadece görüntüleme iznine sahip
                new RolIzinleri {
                    RolId = 2, IzinId = 3,
                    Rol = kullaniciRol, Izin = rotaGoruntulemeIzin,
                    AktifMi = true, SilindiMi = false, EkleyenKullaniciId = "system", EklenmeTarihi = DateTime.UtcNow
                },

                // Moderatör rolü görüntüleme ve düzenleme izinlerine sahip
                new RolIzinleri {
                    RolId = 3, IzinId = 3,
                    Rol = moderatörRol, Izin = rotaGoruntulemeIzin,
                    AktifMi = true, SilindiMi = false, EkleyenKullaniciId = "system", EklenmeTarihi = DateTime.UtcNow
                },
                new RolIzinleri {
                    RolId = 3, IzinId = 5,
                    Rol = moderatörRol, Izin = rotaDuzenlemeIzin,
                    AktifMi = true, SilindiMi = false, EkleyenKullaniciId = "system", EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.RolIzinleri.AddRangeAsync(rolIzinleri);

            // Navigation property'leri güncelle
            if (adminRol != null)
            {
                adminRol.RolIzinleri = rolIzinleri.Where(ri => ri.RolId == 1).ToList();
            }
            if (kullaniciRol != null)
            {
                kullaniciRol.RolIzinleri = rolIzinleri.Where(ri => ri.RolId == 2).ToList();
            }
            if (moderatörRol != null)
            {
                moderatörRol.RolIzinleri = rolIzinleri.Where(ri => ri.RolId == 3).ToList();
            }
        }

        private static async Task SeedRotaKategoriAtamaAsync(RoutesDbContext context)
        {
            // Önce rotaları ve kategorileri al
            var belgradRotasi = await context.Rotalar.FindAsync(1);
            var sultanahmetRotasi = await context.Rotalar.FindAsync(2);
            var dogaKategori = await context.RotaKategoriler.FindAsync(1);
            var sehirKategori = await context.RotaKategoriler.FindAsync(2);

            var rotaKategoriAtamalari = new List<RotaKategoriAtama>
            {
                new RotaKategoriAtama
                {
                    RotaId = 1,
                    KategoriId = 1,
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                },
                new RotaKategoriAtama
                {
                    RotaId = 2,
                    KategoriId = 2,
                    AktifMi = true,
                    SilindiMi = false,
                    EkleyenKullaniciId = "system",
                    EklenmeTarihi = DateTime.UtcNow
                }
            };

            await context.RotaKategoriAtama.AddRangeAsync(rotaKategoriAtamalari);

            // Navigation property'leri güncelle
            if (belgradRotasi != null)
            {
                belgradRotasi.RotaKategoriler = new List<RotaKategoriAtama> { rotaKategoriAtamalari[0] };
            }
            if (sultanahmetRotasi != null)
            {
                sultanahmetRotasi.RotaKategoriler = new List<RotaKategoriAtama> { rotaKategoriAtamalari[1] };
            }
            if (dogaKategori != null)
            {
                dogaKategori.RotaKategoriler = new List<RotaKategoriAtama> { rotaKategoriAtamalari[0] };
            }
            if (sehirKategori != null)
            {
                sehirKategori.RotaKategoriler = new List<RotaKategoriAtama> { rotaKategoriAtamalari[1] };
            }
        }
    }
}