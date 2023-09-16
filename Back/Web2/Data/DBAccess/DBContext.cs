using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using WEB2_Projekat.Models;

namespace WEB2_Projekat.DBAccess
{
    public class DBContext : DbContext
    {
        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<Prodavac> Prodavci { get; set; }

        public DbSet<ArtikalPorudzbina> ArtikliPorudzbina { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Porudzbina>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd(); // Postavljanje automatskog generisanja

            modelBuilder.Entity<Artikal>()
            .HasOne(a => a.Prodavac)//artikal pripada iskljucivo jednom prodavcu
            .WithMany(p => p.Artikli) //prodavac ima vise artikala
            .HasForeignKey(a => a.ProdavacID)//postavlja strani kljuc prodavacid u Artikal tabeli
            .OnDelete(DeleteBehavior.Restrict);//necemo dozvoliti brisanje prodavca ako postoje njegovi artikli

            modelBuilder.Entity<Porudzbina>()
                .HasOne(p => p.Korisnik)
                .WithMany()//korisnik moze da ima vise porudzbina
                .HasForeignKey(p => p.KorisnikId)//porudzbina ima idkorisnika kao strani kljuc
                .OnDelete(DeleteBehavior.Restrict);//necemo dozvoliti prisanje korisnka ako postoje porudzbine vezane za njega
                
        }

    }
}
