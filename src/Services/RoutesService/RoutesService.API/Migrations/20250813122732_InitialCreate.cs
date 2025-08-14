using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RoutesService.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Kurumlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: true),
                    Telefon = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    WebSitesi = table.Column<string>(type: "text", nullable: true),
                    Adres = table.Column<string>(type: "text", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurumlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: true),
                    RotaEklemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RotaGuncellemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RotaSilmeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RolDuzenlemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RolSilmeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RotaYorumSilmeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RotaYorumEklemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RotaYorumGuncellemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RotaKategoriGuncellemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    RotaKategoriSilmeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    YetkiAlaniEklemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    YetkiAlaniGuncellemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    YetkiAlaniSilmeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    YetkiAlaniGoruntulemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    KurumEklemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    KurumGuncellemeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    KurumSilmeYetkisiVarMi = table.Column<bool>(type: "boolean", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotaKategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotaKategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YetkiAlanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: true),
                    KurumId = table.Column<int>(type: "integer", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YetkiAlanlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YetkiAlanlari_Kurumlar_KurumId",
                        column: x => x.KurumId,
                        principalTable: "Kurumlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: true),
                    Soyad = table.Column<string>(type: "text", nullable: true),
                    KullaniciAdi = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Telefon = table.Column<string>(type: "text", nullable: true),
                    Sifre = table.Column<string>(type: "text", nullable: true),
                    SifreHash = table.Column<string>(type: "text", nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanicilar_Roller_RolId",
                        column: x => x.RolId,
                        principalTable: "Roller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rotalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adi = table.Column<string>(type: "text", nullable: true),
                    KategoriId = table.Column<int>(type: "integer", nullable: true),
                    Renk = table.Column<string>(type: "text", nullable: true),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: true),
                    Mesafe = table.Column<double>(type: "double precision", nullable: true),
                    TahminiSureDakika = table.Column<int>(type: "integer", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rotalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rotalar_RotaKategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "RotaKategoriler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RotaOnemliYerler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RotaId = table.Column<int>(type: "integer", nullable: true),
                    Ad = table.Column<string>(type: "text", nullable: true),
                    AciklamaDetay = table.Column<string>(type: "text", nullable: true),
                    Enlem = table.Column<double>(type: "double precision", nullable: true),
                    Boylam = table.Column<double>(type: "double precision", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotaOnemliYerler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RotaOnemliYerler_Rotalar_RotaId",
                        column: x => x.RotaId,
                        principalTable: "Rotalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RotaResimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RotaId = table.Column<int>(type: "integer", nullable: true),
                    ResimAdi = table.Column<string>(type: "text", nullable: true),
                    Resim = table.Column<byte[]>(type: "bytea", nullable: true),
                    ResimUzanti = table.Column<string>(type: "text", nullable: true),
                    ResimBoyut = table.Column<long>(type: "bigint", nullable: true),
                    AciklamaDetay = table.Column<string>(type: "text", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotaResimler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RotaResimler_Rotalar_RotaId",
                        column: x => x.RotaId,
                        principalTable: "Rotalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RotaYorumlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RotaId = table.Column<int>(type: "integer", nullable: true),
                    KullaniciId = table.Column<int>(type: "integer", nullable: true),
                    Puan = table.Column<int>(type: "integer", nullable: true),
                    Yorum = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotaYorumlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RotaYorumlar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RotaYorumlar_Rotalar_RotaId",
                        column: x => x.RotaId,
                        principalTable: "Rotalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Rotalar_KategoriId",
                table: "Rotalar",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_RotaOnemliYerler_RotaId",
                table: "RotaOnemliYerler",
                column: "RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_RotaResimler_RotaId",
                table: "RotaResimler",
                column: "RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_RotaYorumlar_KullaniciId",
                table: "RotaYorumlar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_RotaYorumlar_RotaId",
                table: "RotaYorumlar",
                column: "RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_YetkiAlanlari_KurumId",
                table: "YetkiAlanlari",
                column: "KurumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RotaOnemliYerler");

            migrationBuilder.DropTable(
                name: "RotaResimler");

            migrationBuilder.DropTable(
                name: "RotaYorumlar");

            migrationBuilder.DropTable(
                name: "YetkiAlanlari");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Rotalar");

            migrationBuilder.DropTable(
                name: "Kurumlar");

            migrationBuilder.DropTable(
                name: "Roller");

            migrationBuilder.DropTable(
                name: "RotaKategoriler");
        }
    }
}
