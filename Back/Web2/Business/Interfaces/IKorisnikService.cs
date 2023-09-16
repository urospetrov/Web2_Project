using Data.Repositories;
using Shared.ModelsDTO;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Interfaces
{
    public interface IKorisnikService
    {
        Task<ICollection<Korisnik>> GetAllKorisnike();
        Task<KorisnikRequestModel> GetKorisnik(int idKorisnika);
        Task<Korisnik> Create(KorisnikRequestModel model);
        Task<bool> Delete(int idKorisnika);
        Task<bool> Patch(int idKorisnika,  KorisnikRequestModel model);
        Task<string> Logovanje(LogovanjeDTO dto);
        Task<ICollection<Korisnik>> GetAllKorisnikeProdavceNeverifikovane();
        Task<bool> OdbijVerProdavca(int idKorisnika);
        Task<bool> VerifikujProdavca(int idKorisnika);
        Task<string> GetKorisnikToken(string email, string ime, string prezime, string slikaKorisnika, string tipKorisnika);



    }
}
