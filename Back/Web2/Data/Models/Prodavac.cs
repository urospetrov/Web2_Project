using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB2_Projekat.Models
{
    public class Prodavac
    {
        public int Id { get; set; }
        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public double Postarina { get; set; }
        public bool Verifikovan { get; set; }

        public virtual ICollection<Artikal> Artikli { get; set; }

    }
}
