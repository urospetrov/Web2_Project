using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestModels;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PorudzbinaController : Controller
    {
        private readonly IPorudzbinaService _porudzbinaService;
        public PorudzbinaController(IPorudzbinaService porudzbinaService)
        {
            _porudzbinaService = porudzbinaService;
        }

        [HttpGet("allPorudzbine")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var porudzbine = await _porudzbinaService.GetAllPorudzbine();
            if (porudzbine == null) return BadRequest();
            return Ok(porudzbine);
        }
        [HttpGet]
        public async Task<Porudzbina> GetPorudzbina(int idPorudzbine)
        {
            return await _porudzbinaService.GetPorudzbina(idPorudzbine);
        }
        [HttpPost]
        [Authorize(Roles="Kupac")]
        public async Task<IActionResult> Post(PorudzbinaRequestModel model)
        {
            var porudzbina=await _porudzbinaService.Create(model);
            if (porudzbina == null) return BadRequest();
            return Ok(porudzbina);
        }
        [HttpGet("allPorudzbineKorisnika")]
        [Authorize(Roles ="Kupac")]
        public async Task<IActionResult> GetPorudzbineKorisnika(int idKorisnika)
        {
           var porudzbine=await _porudzbinaService.GetAllPorudzbine(idKorisnika);
            if(porudzbine ==null) return BadRequest(); return Ok(porudzbine);
        }
        [HttpPatch]
        public async Task<bool> Patch(int idPorudzbine, PorudzbinaRequestModel model)
        {
            return await _porudzbinaService.Patch(idPorudzbine, model);
        }
        [HttpDelete]
        public async Task<bool> Delete(int idPorudzbine)
        {
            return await _porudzbinaService.Delete(idPorudzbine);
        }
        
        [HttpGet("allPorudzbineProdavcaStare")]
        [Authorize(Roles ="Prodavac")]
        public async Task<IActionResult> GetPorudzbineProdavcaStare(int korisnikId)//porudzbine sa artiklima od odredjenog prodavca
        {
            var porudzbineProdavca=await _porudzbinaService.GetPorudzbineProdavcaStare(korisnikId);
            if (porudzbineProdavca == null)
                return BadRequest();
            return Ok(porudzbineProdavca);
        }
        [HttpGet("allPorudzbineProdavcaNove")]
        public async Task<ICollection<Porudzbina>> GetPorudzbineProdavcaNove(int korisnikId)
        {
            return await _porudzbinaService.GetPorudzbineProdavcaNove(korisnikId);
        }
        [HttpPost("cancelPorudzbina")]
        public async Task<bool> CancelPorudzbina(PorudzbinaRequestModel porudzbina)
        {
            return await _porudzbinaService.CancelPorudzbina(porudzbina);
        }
        
    }
}
