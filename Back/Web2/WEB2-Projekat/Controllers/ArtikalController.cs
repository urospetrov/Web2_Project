using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestModels;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtikalController : ControllerBase
    {
        private readonly IArtikalService _artikalService;

        public ArtikalController(IArtikalService artikalService)
        {
            _artikalService = artikalService;
        }

        [HttpPost]
        [Authorize(Roles = "Prodavac")]
        public async Task<IActionResult> Post(ArtikalRequestModel model)
        {
            var artikal= await _artikalService.Create(model);
            if (artikal == null) return BadRequest();
            return Ok(artikal);
        }
        [HttpGet]
        [Authorize(Roles ="Kupac")]
        public async Task<IActionResult> Get()
        {
            var artikli = await _artikalService.GetArtikals();
            if(artikli == null) return BadRequest(); return Ok(artikli);
        }
        [HttpGet("idKorisnika")]
        public async Task<IActionResult> Get(int idKorisnika)
        {
            var artikli = await _artikalService.GetAllArtikalsOfProdavac(idKorisnika);
            if (artikli == null)
            {
                return BadRequest();
            }
            else return Ok(artikli);
        }
        [HttpDelete]
        public async Task<bool> Delete(int idArtikla)
        {
            return await _artikalService.Delete(idArtikla);
        }
        [HttpPatch]
        public async Task<bool> Patch(int idArtikla, ArtikalRequestModel model)
        {
            return await _artikalService.Patch(idArtikla, model);
        }
        [HttpPost("UpdateKolicinu")]
        public async Task<bool> UpdateKolicinu(ICollection<ArtikalRequestModel> artikalRequestModels)
        {
            return await _artikalService.UpdateKolicinu(artikalRequestModels);
        }


    }
}
