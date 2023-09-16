using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestModels
{
    public class ProdavacRequestModel
    {
        public int KorisnikId { get; set; }
        public double Postarina { get;set; }
        public bool Verifikovan { get;set; }
        public virtual ICollection<ArtikalRequestModel> Artikli { get; set; } 
    }
}
