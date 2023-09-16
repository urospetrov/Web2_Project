﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEB2_Projekat.DBAccess;

namespace Data.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230916095046_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ArtikalPorudzbina", b =>
                {
                    b.Property<int>("ArtikliId")
                        .HasColumnType("int");

                    b.Property<int>("PorudzbineId")
                        .HasColumnType("int");

                    b.HasKey("ArtikliId", "PorudzbineId");

                    b.HasIndex("PorudzbineId");

                    b.ToTable("ArtikalPorudzbina");
                });

            modelBuilder.Entity("Data.Models.ArtikalPorudzbina", b =>
                {
                    b.Property<int>("ArtikalPorudzbinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArtikalId")
                        .HasColumnType("int");

                    b.Property<int>("ArtikliId")
                        .HasColumnType("int");

                    b.Property<int?>("PorudzbinaId")
                        .HasColumnType("int");

                    b.Property<int>("PorudzbineId")
                        .HasColumnType("int");

                    b.HasKey("ArtikalPorudzbinaId");

                    b.HasIndex("ArtikalId");

                    b.HasIndex("PorudzbinaId");

                    b.ToTable("ArtikliPorudzbina");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Artikal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cena")
                        .HasColumnType("float");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProdavacID")
                        .HasColumnType("int");

                    b.Property<string>("SlikaArtikla")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProdavacID");

                    b.ToTable("Artikli");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Korisnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Postarina")
                        .HasColumnType("float");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SlikaKorisnika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipKorisnika")
                        .HasColumnType("int");

                    b.Property<bool>("Verifikovan")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Porudzbina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdresaDostave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Komentar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<bool>("Otkazana")
                        .HasColumnType("bit");

                    b.Property<DateTime>("VremeIsporuke")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VremePorucivanja")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Porudzbine");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Prodavac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<double>("Postarina")
                        .HasColumnType("float");

                    b.Property<bool>("Verifikovan")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Prodavci");
                });

            modelBuilder.Entity("ArtikalPorudzbina", b =>
                {
                    b.HasOne("WEB2_Projekat.Models.Artikal", null)
                        .WithMany()
                        .HasForeignKey("ArtikliId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB2_Projekat.Models.Porudzbina", null)
                        .WithMany()
                        .HasForeignKey("PorudzbineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.ArtikalPorudzbina", b =>
                {
                    b.HasOne("WEB2_Projekat.Models.Artikal", "Artikal")
                        .WithMany("ArtikalPorudzbina")
                        .HasForeignKey("ArtikalId");

                    b.HasOne("WEB2_Projekat.Models.Porudzbina", "Porudzbina")
                        .WithMany("ArtikalPorudzbina")
                        .HasForeignKey("PorudzbinaId");

                    b.Navigation("Artikal");

                    b.Navigation("Porudzbina");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Artikal", b =>
                {
                    b.HasOne("WEB2_Projekat.Models.Prodavac", "Prodavac")
                        .WithMany("Artikli")
                        .HasForeignKey("ProdavacID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Prodavac");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Porudzbina", b =>
                {
                    b.HasOne("WEB2_Projekat.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Prodavac", b =>
                {
                    b.HasOne("WEB2_Projekat.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Artikal", b =>
                {
                    b.Navigation("ArtikalPorudzbina");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Porudzbina", b =>
                {
                    b.Navigation("ArtikalPorudzbina");
                });

            modelBuilder.Entity("WEB2_Projekat.Models.Prodavac", b =>
                {
                    b.Navigation("Artikli");
                });
#pragma warning restore 612, 618
        }
    }
}
