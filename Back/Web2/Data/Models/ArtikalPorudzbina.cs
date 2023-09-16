using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Data.Models
{
    public class ArtikalPorudzbina
    {
        public int ArtikalPorudzbinaId { get; set; }
        public int ArtikliId { get; set; }
        public int PorudzbineId { get; set; }

        // Navigation properties
        public Artikal Artikal { get; set; }
        public Porudzbina Porudzbina { get; set; }
    }
}
