using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Interfaces
{
    public interface IArtikalService
    {
        Task<Artikal> Create(ArtikalRequestModel model);
        Task<ICollection<Artikal>> GetArtikals();
        Task<ICollection<ArtikalRequestModel>> GetAllArtikalsOfProdavac(int  idKorisnika);
        Task<bool> Delete(int idArtikla);
        Task<bool> Patch(int idArtikla, ArtikalRequestModel model);
        Task<bool> UpdateKolicinu(ICollection<ArtikalRequestModel> artikalRequestModels);


    }
}
