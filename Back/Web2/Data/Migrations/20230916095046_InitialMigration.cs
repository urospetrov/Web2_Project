using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipKorisnika = table.Column<int>(type: "int", nullable: false),
                    SlikaKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verifikovan = table.Column<bool>(type: "bit", nullable: false),
                    Postarina = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    AdresaDostave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VremeIsporuke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Otkazana = table.Column<bool>(type: "bit", nullable: false),
                    VremePorucivanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prodavci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    Postarina = table.Column<double>(type: "float", nullable: false),
                    Verifikovan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prodavci_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdavacID = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlikaArtikla = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikli_Prodavci_ProdavacID",
                        column: x => x.ProdavacID,
                        principalTable: "Prodavci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtikalPorudzbina",
                columns: table => new
                {
                    ArtikliId = table.Column<int>(type: "int", nullable: false),
                    PorudzbineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikalPorudzbina", x => new { x.ArtikliId, x.PorudzbineId });
                    table.ForeignKey(
                        name: "FK_ArtikalPorudzbina_Artikli_ArtikliId",
                        column: x => x.ArtikliId,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtikalPorudzbina_Porudzbine_PorudzbineId",
                        column: x => x.PorudzbineId,
                        principalTable: "Porudzbine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtikliPorudzbina",
                columns: table => new
                {
                    ArtikalPorudzbinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikliId = table.Column<int>(type: "int", nullable: false),
                    PorudzbineId = table.Column<int>(type: "int", nullable: false),
                    ArtikalId = table.Column<int>(type: "int", nullable: true),
                    PorudzbinaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikliPorudzbina", x => x.ArtikalPorudzbinaId);
                    table.ForeignKey(
                        name: "FK_ArtikliPorudzbina_Artikli_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtikliPorudzbina_Porudzbine_PorudzbinaId",
                        column: x => x.PorudzbinaId,
                        principalTable: "Porudzbine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtikalPorudzbina_PorudzbineId",
                table: "ArtikalPorudzbina",
                column: "PorudzbineId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_ProdavacID",
                table: "Artikli",
                column: "ProdavacID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliPorudzbina_ArtikalId",
                table: "ArtikliPorudzbina",
                column: "ArtikalId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliPorudzbina_PorudzbinaId",
                table: "ArtikliPorudzbina",
                column: "PorudzbinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_KorisnikId",
                table: "Porudzbine",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodavci_KorisnikId",
                table: "Prodavci",
                column: "KorisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtikalPorudzbina");

            migrationBuilder.DropTable(
                name: "ArtikliPorudzbina");

            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Prodavci");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
