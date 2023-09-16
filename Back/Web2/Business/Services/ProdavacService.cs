using Business.Interfaces;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.Hosting;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Services
{
    public class ProdavacService: IProdavacService
    {
        private readonly IProdavacRepository _prodavacRepository;
        public ProdavacService(IProdavacRepository prodavacRepository)
        {
            _prodavacRepository= prodavacRepository;
        }

        public async Task<ProdavacRequestModel> Post(ProdavacRequestModel prodavacRequestModel)
        {
            return await _prodavacRepository.Post(prodavacRequestModel);
        }

        public async Task<bool> DeleteProdavac(int idProdavca)
        {
            return await _prodavacRepository.DeleteProdavac(idProdavca);
        }

        public async Task<ICollection<Artikal>> GetAllArtikalsOfProdavac(int idProdavca)
        {
            return await _prodavacRepository.GetAllArtikalsOfProdavac(idProdavca);
        }

        public async Task<ICollection<Prodavac>> GetAllProdavce()
        {
            return await _prodavacRepository.GetAllProdavce();
        }

        public async Task<Prodavac> GetProdavac(int idProdavca)
        {
            return await _prodavacRepository.GetProdavac(idProdavca);
        }

        public async Task<bool> PatchProdavca(int idProdavca, ProdavacRequestModel model)
        {
            return await _prodavacRepository.PatchProdavca(idProdavca, model);
        }
    }
}
