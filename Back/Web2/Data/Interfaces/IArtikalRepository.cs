using Shared.RequestModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Data.Interfaces
{
    public interface IArtikalRepository
    {
        Task<Artikal> Create(ArtikalRequestModel model);
        Task<ICollection<Artikal>> GetAllArtikals();
        Task<ICollection<ArtikalRequestModel>> GetAllArtikalsOfProdavac(int idProdavca);
        Task<bool> Delete(int idArtikla);
        Task<bool> Patch(int idArtikla, ArtikalRequestModel model);
        Task<bool> UpdateKolicinu(ICollection<ArtikalRequestModel> artikalRequestModels);



    }
}
