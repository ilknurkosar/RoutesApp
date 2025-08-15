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
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: true),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: true),
                    EkleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    GuncelleyenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SilenKullaniciId = table.Column<string>(type: "text", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Ad = table.Column<string>(type: "text", nullable: true),
                    Soyad = table.Column<string>(type: "text", nullable: true),
                    KullaniciAdi = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Telefon = table.Column<string>(type: "text", nullable: true),
                    Sifre = table.Column<string>(type: "text", nullable: true),
                    SifreHash = table.Column<string>(type: "text", nullable: true),
                    KurumTanim_Ad = table.Column<string>(type: "text", nullable: true),
                    KurumTanim_Telefon = table.Column<string>(type: "text", nullable: true),
                    KurumTanim_Email = table.Column<string>(type: "text", nullable: true),
                    WebSitesi = table.Column<string>(type: "text", nullable: true),
                    Adres = table.Column<string>(type: "text", nullable: true),
                    Rol_Ad = table.Column<string>(type: "text", nullable: true),
                    RotaKategoriTanim_Ad = table.Column<string>(type: "text", nullable: true),
                    RotaId = table.Column<int>(type: "integer", nullable: true),
                    RotaOnemliYerTanim_Ad = table.Column<string>(type: "text", nullable: true),
                    AciklamaDetay = table.Column<string>(type: "text", nullable: true),
                    Enlem = table.Column<double>(type: "double precision", nullable: true),
                    Boylam = table.Column<double>(type: "double precision", nullable: true),
                    RotaResimTanim_RotaId = table.Column<int>(type: "integer", nullable: true),
                    ResimAdi = table.Column<string>(type: "text", nullable: true),
                    Resim = table.Column<byte[]>(type: "bytea", nullable: true),
                    ResimUzanti = table.Column<string>(type: "text", nullable: true),
                    ResimBoyut = table.Column<long>(type: "bigint", nullable: true),
                    RotaResimTanim_AciklamaDetay = table.Column<string>(type: "text", nullable: true),
                    Adi = table.Column<string>(type: "text", nullable: true),
                    Renk = table.Column<string>(type: "text", nullable: true),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: true),
                    Mesafe = table.Column<double>(type: "double precision", nullable: true),
                    TahminiSureDakika = table.Column<int>(type: "integer", nullable: true),
                    RotaYorumTanim_RotaId = table.Column<int>(type: "integer", nullable: true),
                    KullaniciId = table.Column<int>(type: "integer", nullable: true),
                    Puan = table.Column<int>(type: "integer", nullable: true),
                    Yorum = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    YetkiAlaniTanim_Ad = table.Column<string>(type: "text", nullable: true),
                    YetkiAlaniTanim_Geometry = table.Column<Geometry>(type: "geometry", nullable: true),
                    KurumId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entity_Entity_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Entity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entity_Entity_KurumId",
                        column: x => x.KurumId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entity_Entity_RotaId",
                        column: x => x.RotaId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entity_Entity_RotaResimTanim_RotaId",
                        column: x => x.RotaResimTanim_RotaId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entity_Entity_RotaYorumTanim_RotaId",
                        column: x => x.RotaYorumTanim_RotaId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciRolleri",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "integer", nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciRolleri", x => new { x.KullaniciId, x.RolId });
                    table.ForeignKey(
                        name: "FK_KullaniciRolleri_Entity_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KullaniciRolleri_Entity_RolId",
                        column: x => x.RolId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolIzinleri",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    IzinId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolIzinleri", x => new { x.RolId, x.IzinId });
                    table.ForeignKey(
                        name: "FK_RolIzinleri_Entity_IzinId",
                        column: x => x.IzinId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolIzinleri_Entity_RolId",
                        column: x => x.RolId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RotaKategoriAtama",
                columns: table => new
                {
                    RotaId = table.Column<int>(type: "integer", nullable: false),
                    KategoriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotaKategoriAtama", x => new { x.RotaId, x.KategoriId });
                    table.ForeignKey(
                        name: "FK_RotaKategoriAtama_Entity_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RotaKategoriAtama_Entity_RotaId",
                        column: x => x.RotaId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entity_KullaniciId",
                table: "Entity",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_KurumId",
                table: "Entity",
                column: "KurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_RotaId",
                table: "Entity",
                column: "RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_RotaResimTanim_RotaId",
                table: "Entity",
                column: "RotaResimTanim_RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_RotaYorumTanim_RotaId",
                table: "Entity",
                column: "RotaYorumTanim_RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciRolleri_RolId",
                table: "KullaniciRolleri",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_RolIzinleri_IzinId",
                table: "RolIzinleri",
                column: "IzinId");

            migrationBuilder.CreateIndex(
                name: "IX_RotaKategoriAtama_KategoriId",
                table: "RotaKategoriAtama",
                column: "KategoriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciRolleri");

            migrationBuilder.DropTable(
                name: "RolIzinleri");

            migrationBuilder.DropTable(
                name: "RotaKategoriAtama");

            migrationBuilder.DropTable(
                name: "Entity");
        }
    }
}
