
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.RequestModels
{
    public enum TipKorisnika { Kupac, Prodavac, Odbijen, Admin };
    public class KorisnikRequestModel
    {
        public KorisnikRequestModel(string korisnickoIme, string email, string lozinka, string ime, string prezime, DateTime datumRodjenja, string adresa, TipKorisnika tipKorisnika, string slikaKorisnika, bool verifikovan, double postarina) {
            this.KorisnickoIme = korisnickoIme;
            this.Email = email;
            this.Lozinka = lozinka;
            this.Ime = ime;
            this.Prezime = prezime;
            this.DatumRodjenja = datumRodjenja;
            this.Adresa = adresa;
            this.TipKorisnika= tipKorisnika;
            this.SlikaKorisnika = slikaKorisnika;
            this.Verifikovan    = verifikovan;
            this.Postarina = postarina;
        }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public  TipKorisnika TipKorisnika { get; set; }
        public string SlikaKorisnika { get; set; }
        public bool Verifikovan { get; set; }
        public double Postarina { get; set; }
    }
}
