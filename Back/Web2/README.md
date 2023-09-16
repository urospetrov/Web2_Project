# Web2-Projekat
**_SQLQuery-Korisnik_**

**-- Dodavanje korisnika**

INSERT INTO Korisnici (KorisnickoIme, Email, Ime, Prezime, DatumRodjenja, Adresa,tipKorisnika, Verifikovan, Postarina)
VALUES ('prodavac1', 'prodavac1@example.com', 'Prodavac', 'Jedan', '1990-01-01', 'Adresa 1',1, 1, 10.0);

INSERT INTO Korisnici (KorisnickoIme, Email, Ime, Prezime, DatumRodjenja, Adresa,tipKorisnika, Verifikovan, Postarina)
VALUES ('prodavac2', 'prodavac2@example.com', 'Prodavac', 'Dva', '1995-05-05', 'Adresa 2',1, 1, 15.0);

INSERT INTO Korisnici (KorisnickoIme, Email, Ime, Prezime, DatumRodjenja, Adresa,tipKorisnika, Verifikovan, Postarina)
VALUES ('korisnik1', 'korisnik1@example.com', 'Korisnik', 'Jedan', '1985-03-15', 'Adresa 3',0, 1, 0.0);

**--Dodavanje prodavaca**

INSERT INTO Prodavci (KorisnikId, Postarina, Verifikovan)
VALUES (5, 10.0, 1); //korisnikId=5 to sam iz baze videla

INSERT INTO Prodavci (KorisnikId, Postarina, Verifikovan)
VALUES (6, 15.0, 1);

**-- Dodavanje artikala**

INSERT INTO Artikli(ProdavacID, Naziv, Cena, Kolicina, Opis, SlikaArtikla)
VALUES (1, 'Artikal 1 - Prodavac 1', 100.0, 10, 'Opis artikla 1', 'slika1.jpg');

INSERT INTO Artikli (ProdavacID, Naziv, Cena, Kolicina, Opis, SlikaArtikla)
VALUES (1, 'Artikal 2 - Prodavac 1', 50.0, 5, 'Opis artikla 2', 'slika2.jpg');

INSERT INTO Artikli (ProdavacID, Naziv, Cena, Kolicina, Opis, SlikaArtikla)
VALUES (1, 'Artikal 3 - Prodavac 1', 75.0, 8, 'Opis artikla 3', 'slika3.jpg');

INSERT INTO Artikli (ProdavacID, Naziv, Cena, Kolicina, Opis, SlikaArtikla)
VALUES (2, 'Artikal 1 - Prodavac 2', 120.0, 12, 'Opis artikla 1', 'slika4.jpg');

INSERT INTO Artikli (ProdavacID, Naziv, Cena, Kolicina, Opis, SlikaArtikla)
VALUES (2, 'Artikal 2 - Prodavac 2', 80.0, 7, 'Opis artikla 2', 'slika5.jpg');

INSERT INTO Artikli (ProdavacID, Naziv, Cena, Kolicina, Opis, SlikaArtikla)
VALUES (2

