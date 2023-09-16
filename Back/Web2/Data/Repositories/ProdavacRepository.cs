using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.DBAccess;
using WEB2_Projekat.Models;

namespace Data.Repositories
{
    public class ProdavacRepository : IProdavacRepository
    {
        public DBContext _dBContext;
        
        public ProdavacRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        

        public  async Task<ProdavacRequestModel> Post(ProdavacRequestModel prodavacRequestModel)
        {
            var korisnik = await _dBContext.Korisnici.SingleOrDefaultAsync(k => k.Id == prodavacRequestModel.KorisnikId);

            Prodavac p = new Prodavac();
            p.KorisnikId = korisnik.Id;
            p.Postarina = korisnik.Postarina;
            p.Verifikovan = korisnik.Verifikovan;

             _dBContext.Prodavci.Add(p);
            await _dBContext.SaveChangesAsync();

            var prodavac = await _dBContext.Prodavci.SingleOrDefaultAsync(p=>p.KorisnikId == prodavacRequestModel.KorisnikId);

            ProdavacRequestModel prm= new ProdavacRequestModel();
            prm.Verifikovan = prodavac.Verifikovan;
            prm.KorisnikId = prodavac.KorisnikId;
            prm.Postarina = prodavac.Postarina;
            
            return prm;
        }

        public async Task<bool> DeleteProdavac(int idProdavca)
        {
            var prodavac = await _dBContext.Prodavci.SingleOrDefaultAsync(p => p.Id == idProdavca);
            if (prodavac == null)
            {
                return false;
            }

            var korisnik = await _dBContext.Korisnici.SingleOrDefaultAsync(k => k.Id == prodavac.KorisnikId);
            _dBContext.Korisnici.Remove(korisnik);
            await _dBContext.SaveChangesAsync();
            return true;

        }

        public async Task<ICollection<Artikal>> GetAllArtikalsOfProdavac(int idProdavca)
        {
            await _dBContext.SaveChangesAsync();
            var prodavac=await _dBContext.Prodavci.SingleOrDefaultAsync(p=> p.Id == idProdavca);
            var artikli = prodavac.Artikli.ToList();
            return artikli;

        }

        public async Task<ICollection<Prodavac>> GetAllProdavce()
        {
            await _dBContext.SaveChangesAsync();
            return _dBContext.Prodavci.OrderBy(p => p.Id).ToList();
        }

        public async Task<Prodavac> GetProdavac(int idProdavca)
        {
            var result = await _dBContext.Prodavci.FindAsync(idProdavca);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<bool> PatchProdavca(int idProdavca, ProdavacRequestModel model)
        {
            var prodavac= await _dBContext.Prodavci.FirstOrDefaultAsync(p=> p.Id==idProdavca);
            if(prodavac == null) { return false; }

            prodavac.Postarina = model.Postarina;
            prodavac.Verifikovan=model.Verifikovan;
            
            await _dBContext.SaveChangesAsync();


            return true;
        }
    }
}
