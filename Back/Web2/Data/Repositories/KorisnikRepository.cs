using Data.Interfaces;
using Shared.ModelsDTO;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.DBAccess;
using WEB2_Projekat.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.VisualBasic;
using Shared;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace Data.Repositories
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private DBContext _dbContext;
        public KorisnikRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<bool> OdbijVerProdavca(int idKorisnika)
        {
            try
            {
                var prodavac = await _dbContext.Korisnici.Where(k => k.Id == idKorisnika).FirstOrDefaultAsync();
                prodavac.TipKorisnika = TipKorisnika.Odbijen;
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public async Task<bool> VerifikujProdavca(int idKorisnika)
        {
            try
            {
                var prodavac = await _dbContext.Korisnici.Where(k => k.Id == idKorisnika).FirstOrDefaultAsync();
                prodavac.Verifikovan = true;
                await _dbContext.SaveChangesAsync();

                Prodavac p = new Prodavac();
                p.KorisnikId = prodavac.Id;
                p.Postarina = prodavac.Postarina;
                p.Verifikovan = prodavac.Verifikovan;

                await _dbContext.Prodavci.AddAsync(p);
                await _dbContext.SaveChangesAsync();

                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<ICollection<Korisnik>> GetAllKorisnikeProdavceNeverifikovane()
        {
            var korisnici = _dbContext.Korisnici.OrderBy(k => k.Id).ToList();

            var neverifikovaniProdavci = new List<Korisnik>();

            foreach (var korisnik in korisnici)
            {
                if (korisnik.TipKorisnika == TipKorisnika.Prodavac && korisnik.Verifikovan == false)
                {
                    neverifikovaniProdavci.Add(korisnik);
                }
            }

            await _dbContext.SaveChangesAsync();
            return neverifikovaniProdavci;
        }

        public async Task<Korisnik> Create(KorisnikRequestModel model)
        {
            Korisnik dbEntity = new Korisnik();
            dbEntity.KorisnickoIme = model.KorisnickoIme;
            dbEntity.Email = model.Email;
            dbEntity.Lozinka = BCrypt.Net.BCrypt.HashPassword(model.Lozinka);
            dbEntity.Ime = model.Ime;
            dbEntity.Prezime = model.Prezime;
            dbEntity.DatumRodjenja = model.DatumRodjenja;
            dbEntity.Adresa = model.Adresa;
            dbEntity.TipKorisnika = model.TipKorisnika;
            dbEntity.SlikaKorisnika = model.SlikaKorisnika;
            dbEntity.Verifikovan = model.Verifikovan;
            dbEntity.Postarina = model.Postarina;

            var result = await _dbContext.Korisnici.AddAsync(dbEntity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> Delete(int idKorisnika)
        {
            var korisnik = await _dbContext.Korisnici.FindAsync(idKorisnika);
            if (korisnik == null)
            {
                return false;
            }

            _dbContext.Korisnici.Remove(korisnik);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Korisnik>> GetAllKorisnike()
        {
            await _dbContext.SaveChangesAsync();
            return _dbContext.Korisnici.OrderBy(k => k.Id).ToList();
        }

        public async Task<KorisnikRequestModel> GetKorisnik(int idKorisnika)
        {
            await _dbContext.SaveChangesAsync();
            var k = _dbContext.Korisnici.SingleOrDefault(k => k.Id == idKorisnika);
            KorisnikRequestModel KRM = new KorisnikRequestModel(k.KorisnickoIme, k.Email, k.Lozinka, k.Ime, k.Prezime, k.DatumRodjenja,
                k.Adresa, k.TipKorisnika, k.SlikaKorisnika, k.Verifikovan, k.Postarina);
            return KRM;
        }

        public async Task<string> Logovanje(LogovanjeDTO dto)
        {
            var usernameDTO = dto.Username;
            var lozinkaDTO = dto.Lozinka;
            string tokenString = "";

            var tempKorisnik = _dbContext.Korisnici
                    .Where(korisnik => korisnik.KorisnickoIme == usernameDTO).FirstOrDefault();

            

            try
            {
                var korisnik = _dbContext.Korisnici
                    .Where(korisnik => korisnik.KorisnickoIme == usernameDTO).FirstOrDefault();
                //treba provera da li je uneo dohbru sifru

                if (korisnik == null)
                    throw new Exception($"User with {dto.Username} doesn't exist! Try again.");

                var claims = new[]
                {
                    new Claim("Id", korisnik.Id.ToString()),
                    new Claim("Email", korisnik.KorisnickoIme.ToString()),
                    new Claim(ClaimTypes.Role, korisnik.TipKorisnika.ToString()),
                    new Claim("Verifikovan", korisnik.Verifikovan.ToString())
                };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Shared.Constants.SECRET_KEY));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44388",
                    claims: claims,
                    expires:DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );
                tokenString=new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return tokenString;

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return tokenString;
                //hendluj ovu gresku ako nisu dobro kredencijali uneseni
            }



        }

        public async Task<bool> Patch(int idKorisnika, KorisnikRequestModel model)
        {
            var korisnik = await _dbContext.Korisnici.FindAsync(idKorisnika);
            if (korisnik == null)
            {
                return false;
            }
            korisnik.Ime = model.Ime;
            korisnik.Prezime = model.Prezime;
            korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(model.Lozinka);
            korisnik.SlikaKorisnika = model.SlikaKorisnika;
            korisnik.Verifikovan = model.Verifikovan;
            korisnik.Adresa = model.Adresa;
            korisnik.DatumRodjenja = model.DatumRodjenja;
            korisnik.Email = model.Email;
            korisnik.Postarina = model.Postarina;
            korisnik.KorisnickoIme = model.KorisnickoIme;

            await _dbContext.SaveChangesAsync();
            return true;


        }

        public async Task<string> GetKorisnikToken(string email, string ime, string prezime, string slikaKorisnika, string tipKorisnika)
        {
            string tokenString = "";
            string i = ime.ToLower();
            var temp = await _dbContext.Korisnici
                    .Where(korisnik => korisnik.Email == email && korisnik.Ime.ToLower() == i).FirstOrDefaultAsync();
            if (temp == null)
            {
                Korisnik dbEntity=new Korisnik();
                dbEntity.Email = email;
                dbEntity.Ime = ime;
                dbEntity.Prezime=prezime;
                dbEntity.SlikaKorisnika = slikaKorisnika;
                dbEntity.KorisnickoIme = ime;
                dbEntity.Verifikovan = false;
                dbEntity.Adresa = "";
                if (tipKorisnika == "Kupac")
                {
                    dbEntity.TipKorisnika = TipKorisnika.Kupac;
                    await _dbContext.Korisnici.AddAsync(dbEntity);
                    await _dbContext.SaveChangesAsync();
                }else if (tipKorisnika == "Prodavac")
                {
                    dbEntity.TipKorisnika = TipKorisnika.Prodavac;
                    await _dbContext.Korisnici.AddAsync(dbEntity);
                    await _dbContext.SaveChangesAsync();
                }

            }
            var tempKorisnik = await _dbContext.Korisnici
                    .Where(korisnik => korisnik.Email == email && korisnik.Ime.ToLower() == i).FirstOrDefaultAsync();
            var claims = new[]
                {
                    new Claim("Id", tempKorisnik.Id.ToString()),
                    new Claim("Email", tempKorisnik.KorisnickoIme.ToString()),
                    new Claim(ClaimTypes.Role, tempKorisnik.TipKorisnika.ToString()),
                    new Claim("Verifikovan", tempKorisnik.Verifikovan.ToString())
                };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Shared.Constants.SECRET_KEY));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:44388",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            tokenString=new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
