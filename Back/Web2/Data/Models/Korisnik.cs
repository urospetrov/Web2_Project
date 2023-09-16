using Shared.RequestModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace WEB2_Projekat.Models
{
    
    public class Korisnik
    {
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        
        public TipKorisnika TipKorisnika { get; set; }

        public string SlikaKorisnika { get; set; }
        public bool Verifikovan { get; set; }
        public double Postarina { get; set; } //prodavac, svaki ima svoju postarinu

    }
}
