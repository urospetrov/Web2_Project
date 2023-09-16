using Business.Interfaces;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
using Shared.ModelsDTO;
using Shared.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikService _korisnikService;

        public KorisnikController(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
        }

        [HttpGet("neverProdavce")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllKorisnikeProdavceNeverifikovane()
        {
            var korisniciProdavci = await _korisnikService.GetAllKorisnikeProdavceNeverifikovane();
            if (korisniciProdavci == null)
                return BadRequest();
            return Ok(korisniciProdavci);
        }

        [HttpPost("neverProdavca")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OdbijVerProdavca(int idKorisnika)
        {
            var temp=await _korisnikService.OdbijVerProdavca(idKorisnika);
            if(temp==false) return BadRequest();
            return Ok(temp);
        }
        [HttpPost("verProdavca")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> VerifikujProdavca(int idKorisnika)
        {
            var temp=await _korisnikService.VerifikujProdavca(idKorisnika);
            if(temp==false) return BadRequest();
            return Ok(temp);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Korisnik> Post( KorisnikRequestModel model)
        {
            var noviKorisnik = await _korisnikService.Create(model);
            return noviKorisnik;
        }


        [HttpGet("allKorisnike")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Get() //get all users
        {
            var korisnici = await _korisnikService.GetAllKorisnike();
            if (korisnici == null) BadRequest(); 
            return Ok(korisnici);
        }

        [HttpGet]
        [Authorize(Roles ="Admin, Kupac, Prodavac")]
        public async Task<IActionResult> Get(int idKorisnika)
        {
            var korisnik = await _korisnikService.GetKorisnik(idKorisnika);
            if(korisnik == null) return BadRequest();
            return Ok(korisnik);
        }

        [AllowAnonymous]
        [HttpPost("logovanje")]
        public async Task<IActionResult> Logovanje(LogovanjeDTO dto)
        {
            string token = await _korisnikService.Logovanje(dto);
            return Ok(new {Token=token});
        }

        [HttpDelete]
        public async Task<bool> Delete(int idKorisnika)
        {
            return await _korisnikService.Delete(idKorisnika);
        }
        [HttpPut]
        [Authorize(Roles = "Kupac, Prodavac, Admin")]
        public async Task<IActionResult> Put(int idKorisnika, KorisnikRequestModel model)
        {
            var temp= await _korisnikService.Patch(idKorisnika,model);
            if (temp == false) return BadRequest();
            return Ok(temp);
        }

        [HttpGet("getKorisnikToken")]
        public async Task<IActionResult> GetKorisnikToken(string email, string ime, string prezime, string slikaKorisnika, string tipKorisnika)
        {
            var token=await _korisnikService.GetKorisnikToken(email, ime,prezime,slikaKorisnika, tipKorisnika);
            var responseObject = new { token };
            return Ok(responseObject);

        }


    }
}
