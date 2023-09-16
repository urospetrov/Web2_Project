using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Interfaces
{
    public interface IPorudzbinaService
    {
        Task<Porudzbina> Create(PorudzbinaRequestModel model);
        Task<Porudzbina> GetPorudzbina(int idPorudzbinaId);
        //sve porudzbine korisnika
        Task<ICollection<PorudzbinaRequestModel>> GetAllPorudzbine(int idKorisnika);
        //sve porudzbine generalno
        Task<ICollection<Porudzbina>> GetAllPorudzbine();
        Task<bool> Patch(int idPorudzbine, PorudzbinaRequestModel model);
        Task<bool> Delete(int idPorudzbine);
        Task<ICollection<Porudzbina>> GetPorudzbineProdavcaStare(int korisnikId);
        Task<ICollection<Porudzbina>> GetPorudzbineProdavcaNove(int korisnikId);
        Task<bool> CancelPorudzbina(PorudzbinaRequestModel porudzbina);
    }
}
