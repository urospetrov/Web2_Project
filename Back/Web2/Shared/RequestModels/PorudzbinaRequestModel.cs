using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestModels
{
    public class PorudzbinaRequestModel
    {
        public PorudzbinaRequestModel()
        {
            Artikli = new List<ArtikalRequestModel>();
        }
        public int Id { get; set; }
        public int KorisnikId { get; set; }
        public string AdresaDostave { get; set; }
        public string Komentar { get; set; }

        public ICollection<ArtikalRequestModel> Artikli { get; set; }

        public DateTime VremeIsporuke { get; set; }
        public bool Otkazana {get;set;}
        public DateTime VremePorucivanja { get; set; }


    }
}
