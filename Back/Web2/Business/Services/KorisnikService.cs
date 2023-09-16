using Business.Interfaces;
using Data.Interfaces;
using Data.Repositories;
using Shared.ModelsDTO;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IKorisnikRepository _korisnikRepository;
        public KorisnikService(IKorisnikRepository korisnikRepository)
        {
            _korisnikRepository = korisnikRepository;
        }

        public async Task<bool> OdbijVerProdavca(int idKorisnika)
        {
            return await _korisnikRepository.OdbijVerProdavca(idKorisnika);
        }
        public async Task<bool> VerifikujProdavca(int idKorisnika)
        {
            return await _korisnikRepository.VerifikujProdavca(idKorisnika);
        }
        public async Task<ICollection<Korisnik>> GetAllKorisnikeProdavceNeverifikovane()
        {
            return await _korisnikRepository.GetAllKorisnikeProdavceNeverifikovane();
        }

        public async Task<Korisnik> Create(KorisnikRequestModel model)
        {
            return await _korisnikRepository.Create(model);
        }

        public async Task<bool> Delete(int idKorisnika)
        {
            return await _korisnikRepository.Delete(idKorisnika);
        }

        public async Task<ICollection<Korisnik>> GetAllKorisnike()
        {
            return await _korisnikRepository.GetAllKorisnike();
        }

        public async Task<KorisnikRequestModel> GetKorisnik(int idKorisnika)
        {
            return await _korisnikRepository.GetKorisnik(idKorisnika);
        }

        public async Task<string> Logovanje(LogovanjeDTO dto)
        {
            return await _korisnikRepository.Logovanje(dto);
        }

        public async Task<bool> Patch(int idKorisnika, KorisnikRequestModel model)
        {
            return await _korisnikRepository.Patch(idKorisnika, model);
        }

        public async Task<string> GetKorisnikToken(string email, string ime, string prezime, string slikaKorisnika, string tipKorisnika)
        {
            return await _korisnikRepository.GetKorisnikToken(email,ime,prezime, slikaKorisnika, tipKorisnika);
        }
    }
}
