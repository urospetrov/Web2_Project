using Business.Interfaces;
using Data.Interfaces;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Services
{
    public class ArtikalService : IArtikalService
    {
        private readonly IArtikalRepository _artikalRepository;
        public ArtikalService(IArtikalRepository artikalRepository) 
        {
            _artikalRepository = artikalRepository;
        }

        public async Task<Artikal> Create(ArtikalRequestModel model)
        {
            return await _artikalRepository.Create(model);
        }

        public async Task<bool> Delete(int idArtikla)
        {
            return await _artikalRepository.Delete(idArtikla);
        }

        public async Task<ICollection<ArtikalRequestModel>> GetAllArtikalsOfProdavac(int idProdavca)
        {
            return await _artikalRepository.GetAllArtikalsOfProdavac(idProdavca);
        }

        public async Task<ICollection<Artikal>> GetArtikals()
        {
            return await _artikalRepository.GetAllArtikals();
        }

        public async Task<bool> Patch(int idArtikla, ArtikalRequestModel model)
        {
            return await _artikalRepository.Patch(idArtikla, model);
        }

        public async Task<bool> UpdateKolicinu(ICollection<ArtikalRequestModel> artikalRequestModels)
        {
            return await _artikalRepository.UpdateKolicinu(artikalRequestModels); 
        }
    }
}
