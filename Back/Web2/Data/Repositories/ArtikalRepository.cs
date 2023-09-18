using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    public class ArtikalRepository : IArtikalRepository
    {
        private DBContext _dbContext;

        public ArtikalRepository(DBContext dBContext) 
        {
            _dbContext = dBContext;
        }
        //nzm dal mi treba ova metoda uopste
        

        public async Task<Artikal> Create(ArtikalRequestModel model)
        {
            //try
            //{

            var prodavac = await _dbContext.Prodavci.FirstOrDefaultAsync(p => p.KorisnikId == model.ProdavacId);
            //}catch(Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}


            Artikal dbEntity = new Artikal();

            dbEntity.ProdavacID = prodavac.Id;
            dbEntity.Naziv = model.Naziv;
            dbEntity.Cena = model.Cena;
            dbEntity.Kolicina = model.Kolicina;
            dbEntity.Opis = model.Opis;
            dbEntity.SlikaArtikla = model.Slika;

            var result = await _dbContext.Artikli.AddAsync(dbEntity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> Delete(int idArtikla)
        {
            var artikal = await _dbContext.Artikli.FindAsync(idArtikla);
            if (artikal == null)
            {
                return false;
            }

            _dbContext.Artikli.Remove(artikal);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Artikal>> GetAllArtikals()
        {
            await _dbContext.SaveChangesAsync();
            return _dbContext.Artikli.OrderBy(a=>a.Id).ToList();
        }

        public async Task<ICollection<ArtikalRequestModel>> GetAllArtikalsOfProdavac(int idKorisnik)
        {
            var prodavac = await _dbContext.Prodavci.Where(p => p.KorisnikId == idKorisnik).FirstOrDefaultAsync();
            var artikli = await _dbContext.Artikli.Where(a => a.ProdavacID == prodavac.Id).ToListAsync();
            var artikliFinal=new List<ArtikalRequestModel>();

            foreach(var a in artikli)
            {
                ArtikalRequestModel ar=new ArtikalRequestModel();
                ar.Id = a.Id;
                ar.ProdavacId = prodavac.Id;
                ar.Naziv=a.Naziv;
                ar.Cena=a.Cena;
                ar.Kolicina =a.Kolicina;
                ar.Opis=a.Opis;
                ar.Slika = a.SlikaArtikla;

                artikliFinal.Add(ar);
            }

            return artikliFinal;
        }

        public async Task<bool> Patch(int idArtikla, ArtikalRequestModel model)
        {
            var artikal = await _dbContext.Artikli.FindAsync(idArtikla);
            if (artikal==null)
            {
                return false;
            }

            //ne moze da se menja idProdavca
            artikal.Opis= model.Opis;
            artikal.Naziv= model.Naziv;
            artikal.Cena= model.Cena;
            artikal.Kolicina= model.Kolicina;
            artikal.SlikaArtikla = model.Slika;

            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateKolicinu(ICollection<ArtikalRequestModel> artikalRequestModels)
        {
            bool nasao = false;
            var artikli = await _dbContext.Artikli.ToListAsync();
            

            try
            {
                foreach(var artikal in artikli)
                {
                    foreach(var model in artikalRequestModels)
                    {
                        if (artikal.Id == model.Id)
                        {
                            nasao = true;
                            artikal.Kolicina = artikal.Kolicina - 1;

                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
            //ovde upada iako ne nadje
            if(!nasao)
            {
                return false;//nije nasao ni jedan pogodak, nista nije promenio
            }
            return true;
        }
    }
}
