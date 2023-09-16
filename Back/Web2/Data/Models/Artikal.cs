using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB2_Projekat.Models
{
    public class Artikal
    {
        public int Id { get; set; }

        public int ProdavacID { get; set; }
        public virtual Prodavac Prodavac { get; set; }

        public string Naziv { get; set; }
        public double Cena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public string SlikaArtikla { get; set; }

        public virtual ICollection<Porudzbina> Porudzbine { get; set; }

        //referenca na ArtikalPorudzbina
        public virtual ICollection<ArtikalPorudzbina> ArtikalPorudzbina { get; set; }

    }
}
