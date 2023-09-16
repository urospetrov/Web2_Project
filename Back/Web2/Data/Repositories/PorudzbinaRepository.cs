using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.DBAccess;
using WEB2_Projekat.Models;

namespace Data.Repositories
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private DBContext _dbContext;
        public PorudzbinaRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<Porudzbina> CreatePorudzbina(PorudzbinaRequestModel model)
        {
            var interval = TimeSpan.FromHours(2);
            
            Porudzbina porudzbina = new Porudzbina();
            porudzbina.Id= model.Id;
            porudzbina.KorisnikId = model.KorisnikId;
            porudzbina.AdresaDostave=model.AdresaDostave;
            porudzbina.Komentar=model.Komentar;
            porudzbina.VremeIsporuke = model.VremeIsporuke.Add(interval);
            porudzbina.VremePorucivanja = model.VremePorucivanja.Add(interval);

            // Add the new Porudzbina to the context
            try
            {
                var result = await _dbContext.Porudzbine.AddAsync(porudzbina);
                await _dbContext.SaveChangesAsync();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ICollection<Artikal> artikliDB = _dbContext.Artikli.ToList();
            ICollection<ArtikalRequestModel> artikliFront = model.Artikli;
            List<Artikal> final = new List<Artikal>();

            foreach (var af in artikliFront)
            {
                var artikal = artikliDB.FirstOrDefault(adb => adb.Naziv == af.Naziv);
                if (artikal != null)
                {
                    var artikalPorudzbina = new ArtikalPorudzbina
                    {
                        ArtikliId = artikal.Id,
                        PorudzbineId = porudzbina.Id // Use the Id of the newly created Porudzbina
                    };
                    final.Add(artikal);
                    try
                    {
                        _dbContext.Set<ArtikalPorudzbina>().Add(artikalPorudzbina);
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            foreach(var artikal in final)
            {
                porudzbina.Artikli.Add(artikal);
            }



            return porudzbina;
        }

        public async Task<bool> DeletePorudzbina(int idPorudzbine)
        {
            var porudzbina = await _dbContext.Porudzbine.SingleOrDefaultAsync(p => p.Id == idPorudzbine); //nije dobro jer vraca true ili false
            if (porudzbina == null)
            {
                return false;
            }


            _dbContext.Porudzbine.Remove(porudzbina);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<PorudzbinaRequestModel>> GetAllPorudzbine(int idKorisnika)
        {
            var porudzbine = _dbContext.Porudzbine.Where(p => p.KorisnikId == idKorisnika).ToList();
            var artPor =  _dbContext.ArtikliPorudzbina.ToList();
            List<PorudzbinaRequestModel> prmList = new List<PorudzbinaRequestModel>();

            foreach(var p in porudzbine)
            {
                foreach(var ap in artPor)
                {
                    if(p.Id==ap.PorudzbineId)
                    {
                        var artikal=await _dbContext.Artikli.Where(a=>a.Id==ap.ArtikliId).SingleOrDefaultAsync();
                        p.Artikli.Add(artikal);
                    }
                }
            }
            //prepakovanje
            foreach(var p in porudzbine)
            {
                PorudzbinaRequestModel prm= new PorudzbinaRequestModel();
                prm.Id = p.Id;
                prm.KorisnikId = p.KorisnikId;
                prm.AdresaDostave= p.AdresaDostave;
                prm.Komentar= p.Komentar;
                foreach(var a in p.Artikli)
                {
                    ArtikalRequestModel arm= new ArtikalRequestModel();
                    arm.Id= a.Id;
                    arm.ProdavacId = a.ProdavacID;
                    arm.Naziv= a.Naziv;
                    arm.Cena= a.Cena;
                    arm.Kolicina= a.Kolicina;
                    arm.Opis=a.Opis;
                    arm.Slika = a.SlikaArtikla;

                    prm.Artikli.Add(arm);
                }
                prm.VremeIsporuke = p.VremeIsporuke;
                prm.Otkazana = p.Otkazana;

                prmList.Add(prm);

            }
            return prmList;
           
        }

        public async Task<ICollection<Porudzbina>> GetAllPorudzbine()
        {
            await _dbContext.SaveChangesAsync();
            return _dbContext.Porudzbine.OrderBy(p => p.Id).ToList();
        }

        public async Task<Porudzbina> GetPorudzbina(int idPorudzbinaId)
        {
            var result = await _dbContext.Porudzbine.SingleOrDefaultAsync(p=> p.Id==idPorudzbinaId);
            if(result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<bool> PatchPorudzbina(int idPorudzbine, PorudzbinaRequestModel model)
        {
            var porudzbina = await _dbContext.Porudzbine.SingleOrDefaultAsync(p=> p.Id==idPorudzbine);
            if (porudzbina == null)
            {
                return false;
            }

            porudzbina.AdresaDostave= model.AdresaDostave;
            porudzbina.Komentar=model.Komentar; 

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Porudzbina>> GetPorudzbineProdavcaStare(int korisnikId)
        {
            var artikliProdavca = new List<Artikal>();
            var temp=new List<Porudzbina>();

            var korisnik= await _dbContext.Korisnici.SingleOrDefaultAsync(k=>k.Id==korisnikId);
            var prodavac = await _dbContext.Prodavci.SingleOrDefaultAsync(p => p.KorisnikId == korisnikId);
            var sviArtikli = await _dbContext.Artikli.ToListAsync();

            if (prodavac == null)
            {
                return temp;
            }

            foreach(var a in sviArtikli)
            {
                if (a.ProdavacID == prodavac.Id)
                {
                    artikliProdavca.Add(a);
                }
            }

            var svePorudzbine= await _dbContext.Porudzbine.OrderBy(a => a.Id).ToListAsync();
            var sviArtikalPorudzbine = await _dbContext.ArtikliPorudzbina.ToListAsync();

            var idPorudzbinaZaPrikazati = new HashSet<int>();

            foreach(var ap in sviArtikalPorudzbine)
            {
                foreach (var a in artikliProdavca)
                {
                    if(ap.ArtikliId == a.Id)
                    {
                        idPorudzbinaZaPrikazati.Add(ap.PorudzbineId); //id-jevi porudzbina koje treba da prikazemo
                    }
                }
            }

            var porudzbineZaPrikazati=new List<Porudzbina>();

            foreach(var sp in svePorudzbine)
            {
                foreach(var id in idPorudzbinaZaPrikazati)
                {
                    if(sp.Id == id)
                    {
                        porudzbineZaPrikazati.Add(sp);
                    }
                }
            }

            return porudzbineZaPrikazati;
        }
        public async Task<ICollection<Porudzbina>> GetPorudzbineProdavcaNove(int korisnikId)
        {
            var artikliProdavca = new List<Artikal>();
            var temp=new List<Porudzbina>();

            var korisnik = await _dbContext.Korisnici.SingleOrDefaultAsync(k => k.Id == korisnikId);
            var prodavac = await _dbContext.Prodavci.SingleOrDefaultAsync(p => p.KorisnikId == korisnikId);
            var sviArtikli = await _dbContext.Artikli.ToListAsync();

            if (prodavac == null)
            {
                return temp;
            }

            foreach (var a in sviArtikli)
            {
                if (a.ProdavacID == prodavac.Id)
                {
                    artikliProdavca.Add(a);
                }
            }

            var svePorudzbine = await _dbContext.Porudzbine.OrderBy(a => a.Id).ToListAsync();
            var sviArtikalPorudzbine = await _dbContext.ArtikliPorudzbina.ToListAsync();

            var idPorudzbinaZaPrikazati = new HashSet<int>();

            foreach (var ap in sviArtikalPorudzbine)
            {
                foreach (var a in artikliProdavca)
                {
                    if (ap.ArtikliId == a.Id)
                    {
                        idPorudzbinaZaPrikazati.Add(ap.PorudzbineId); //id-jevi porudzbina koje treba da prikazemo
                    }
                }
            }

            var porudzbineZaPrikazati = new List<Porudzbina>();

            foreach (var sp in svePorudzbine)
            {
                foreach (var id in idPorudzbinaZaPrikazati)
                {
                    if (sp.Id == id)
                    {
                        porudzbineZaPrikazati.Add(sp);
                    }
                }
            }

            List<Porudzbina> final= new List<Porudzbina>();


            foreach(var pzp in porudzbineZaPrikazati)
            {
                string zadatoVremeString = pzp.VremeIsporuke.ToString();
                string formatiraniString = DateTime.ParseExact(zadatoVremeString, "dd/MM/yyyy HH:mm:ss", null).ToString("yyyy-MM-dd HH:mm:ss");
                DateTime zadatoVreme = DateTime.ParseExact(formatiraniString, "yyyy-MM-dd HH:mm:ss", null);
                DateTime trenutnoVreme = DateTime.Now;
                TimeSpan protekloVreme=trenutnoVreme- zadatoVreme;
                if (protekloVreme.TotalHours <= 1)
                {
                    final.Add(pzp);
                }
            }
            return final;
        }

        public async Task<bool> CancelPorudzbina(PorudzbinaRequestModel porudzbina)
        {
            var porudzbine = await _dbContext.Porudzbine.ToListAsync();
            
            foreach(var p in porudzbine)
            {
                if (p.Id == porudzbina.Id)
                {
                    if (!p.Otkazana)
                    {
                        p.Otkazana = true;
                        await _dbContext.SaveChangesAsync();
                        
                    }
                    else
                    {
                        throw new Exception("Ne mozete otkazati vec otkazanu porudzbinu");
                        
                    }
                }
            }

            //ovde bih mogla da povecam kolicine
            var artikliIzPorudzbine =porudzbina.Artikli; //treba proveriti da li ce iste artikle vise puta ovde prikazati
            var artikli= await _dbContext.Artikli.ToListAsync();

            foreach (var a in artikli)
            {
                foreach(var aip in artikliIzPorudzbine)
                {
                    if(a.Id== aip.Id)
                    {
                        a.Kolicina = a.Kolicina + 1;
                        await _dbContext.SaveChangesAsync();
                    }
                }

            }


            return true;
        }
    }
}
