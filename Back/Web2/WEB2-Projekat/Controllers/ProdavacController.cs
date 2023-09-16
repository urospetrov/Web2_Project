using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdavacController : Controller
    {
        private readonly IProdavacService _prodavacService;
        public ProdavacController(IProdavacService prodavacService)
        {
            _prodavacService = prodavacService;
        }
        
        [HttpGet("allProdavci")]
        public async Task<ICollection<Prodavac>> Get()
        {
            return await _prodavacService.GetAllProdavce();
        }
        [HttpGet]
        public async Task<Prodavac> GetProdavca(int idProdavca)
        {
            return await _prodavacService.GetProdavac(idProdavca);
        }
        [HttpPost]

        public async Task<ProdavacRequestModel> Post(ProdavacRequestModel model)
        {
            return await _prodavacService.Post(model);
        }
        
        [HttpGet("allArtikalsOfProdavac")]
        public async Task<ICollection<Prodavac>> GetAllArtikalsOfProdavac(int idProdavca)
        {
            return (ICollection<Prodavac>)await _prodavacService.GetAllArtikalsOfProdavac(idProdavca);
        }
        [HttpPatch]
        public async Task<bool> Patch(int idprodavca, ProdavacRequestModel model)
        {
            return await _prodavacService.PatchProdavca(idprodavca, model);
        }
        [HttpDelete]
        public async Task<bool> Delete(int idProdavca)
        {
            return await _prodavacService.DeleteProdavac(idProdavca);
        }
    }
}
