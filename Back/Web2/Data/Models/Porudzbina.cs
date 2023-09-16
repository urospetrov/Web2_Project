using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace WEB2_Projekat.Models
{
    public class Porudzbina
    {
        public int Id { get; set; }

        // ID korisnika
        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

        private ICollection<Artikal> _artikli;
        public virtual ICollection<Artikal> Artikli
        {
            get => _artikli ?? (_artikli = new List<Artikal>());
            set => _artikli = value;
        }

        public string AdresaDostave { get; set; }
        public string Komentar { get; set; }

        public DateTime VremeIsporuke { get; set; }
        //referenca na artikalPorudzbina tabelu
        public virtual ICollection<ArtikalPorudzbina> ArtikalPorudzbina { get; set; }

        public bool Otkazana { get;set; }
        public DateTime VremePorucivanja { get; set; }


    }

}
