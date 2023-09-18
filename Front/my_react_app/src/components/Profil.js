import React, { useEffect, useState, useRef } from "react";
import ImageUploader from "../services/KorisnikService";
import jwtDecode from "jwt-decode";
import { Link } from "react-router-dom";

//testiramo dal se komituje na master

const Profil = () => {
  const [formData, setFormData] = useState({
    korisnickoIme: "",
    email: "",
    lozinka: "",
    ime: "",
    prezime: "",
    datumRodjenja: "2000-01-01",
    adresa: "",
    tipKorisnika: 0,
    slikaKorisnika: "",
    verifikovan: true,
    postarina: 0,
  });

  //state za cuvanje podatka sa server
  const [korisnikPodaci, setKorisnikPodaci] = useState(null);

  //state za cuvanje tipaKorisnika
  const tipRef = useRef(0);
  const postarinaRef = useRef(0);

  const formRef = useRef(null);

  const [passwordConfirmation, setPasswordConfirmation] = useState("");
  const [passwordsMatch, setPasswordsMatch] = useState(true);
  const [isSuccess, setIsSuccess] = useState(false);

  //rad sa slikom
  const [UploadedImage, setUploadedImage] = useState(null);
  const handleImageUpload = (imageData) => {
    setUploadedImage(imageData);
  };

  var token = localStorage.getItem("token");
  const decodedToken = jwtDecode(token);
  console.log(decodedToken["Id"]);
  useEffect(() => {
    fetch(
      `https://localhost:44388/Korisnik?idKorisnika=${decodedToken["Id"]}`,
      {
        //zakucala jednog korisnika
        method: "GET",
        // body: JSON.stringify(formData), //ne mogu slati body u get zahtevu
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        mode: "cors",
      }
    )
      .then((Response) => Response.json())
      .then((data) => {
        //obrada odgovora servera
        console.log(data);
        tipRef.current = data.tipKorisnika;
        postarinaRef.current = data.postarinaRef;
        //foramtiranje datuma
        const datumRodjenja = data.datumRodjenja; //2023-07-25T00:00:00
        const formattedDateString = datumRodjenja.split("T")[0];
        data.datumRodjenja = formattedDateString;
        //console.log(formattedDateString+" ******db->forma");
        setKorisnikPodaci(data);
        setFormData(data);
      })
      .catch((error) => {
        //obrada greske
        console.error(
          "Greška prilikom dobavljanja podataka sa servera:",
          error
        );
      });
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });

    if (name === "lozinka") {
      // Ako se menja polje za šifru, ažuriraj i polje za potvrdu šifre
      setPasswordConfirmation("");
    }
  };

  const handlePasswordConfirmationChange = (e) => {
    const confirmedPassword = e.target.value;
    setPasswordConfirmation(e.target.value);

    // Proveri da li unete šifre i potvrda šifre odgovaraju
    if (formData.lozinka === confirmedPassword) {
      setPasswordsMatch(true);
    } else {
      setPasswordsMatch(false);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Fetch zahtev za slanje izmenjenih podataka na server
    //formatiranje datuma za slanje nazad serveru
    if (formData.lozinka !== passwordConfirmation) {
      console.log("Šifre se ne podudaraju.");
      return;
    }

    const zaKonvertovanje = formData.datumRodjenja; //"28-7-2023"
    const formattedDateString = zaKonvertovanje + "T23:00:00.000Z";

    const formDataToSend = {
      korisnickoIme: formRef.current.korisnickoIme.value,
      email: formRef.current.email.value,
      lozinka: formRef.current.lozinka.value,
      ime: formRef.current.ime.value,
      prezime: formRef.current.prezime.value,
      datumRodjenja: formattedDateString,
      adresa: formRef.current.adresa.value,
      tipKorisnika: tipRef.current.value,
      slikaKorisnika: UploadedImage,
      postarina: postarinaRef.current,
    };

    fetch(
      `https://localhost:44388/Korisnik?idKorisnika=${decodedToken["Id"]}`,
      {
        method: "PUT",
        body: JSON.stringify(formDataToSend),
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        mode: "cors",
      }
    )
      .then((response) => response.json())
      .then((data) => {
        console.log("Podaci su uspešno poslati na server:", data);
        setIsSuccess(true);
      })
      .catch((error) => {
        // Obrada greške
        console.error("Greška prilikom slanja podataka na server:", error);
        setIsSuccess(false);
      });
  };
  
  const containerStyle = {
    display: 'inline-flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    width: '400px', // or 'inline-block' or 'inline-flex' depending on your needs
  };

  const innerContainerStyle = {
    width: '50%',
  }

  const labelStyle = {
    whiteSpace: 'nowrap',
  };

  return (
    <div className="container mt-5">
      <div className="row">
        <div className="col-lg-6">
          <h2 className="mb-4" style={{ color: "#279980" }}>
            Profil
          </h2>
          {korisnikPodaci ? (
            <form onSubmit={handleSubmit} ref={formRef} className="mt-4">
              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                    <label style={labelStyle}>
                      Korisnicko ime:
                    </label>
                  </div>
                  <div style={innerContainerStyle}>
                    <input
                      type="text"
                      name="korisnickoIme"
                      value={formData.korisnickoIme}
                      className="form-control"
                      onChange={handleChange}
                    />
                  </div>
                </div>
              </div>
              <br/>

              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                    <label style={labelStyle}>
                      E-mail:
                    </label>
                  </div>
                  <div style={innerContainerStyle}>
                    <input
                      type="email"
                      name="email"
                      value={formData.email}
                      onChange={handleChange}
                      className="form-control"
                    />
                  </div>
                </div>
              </div>
              <br/>

              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                    <label>
                      Lozinka:
                    </label>
                  </div>
                  <div style={innerContainerStyle}>
                    <input
                      type="password"
                      name="lozinka"
                      value={formData.lozinka}
                      onChange={handleChange}
                      className="form-control"
                    />
                  </div>
                </div>
              </div>
              <br/>

              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                    <label>
                      Potvrda lozinke:
                    </label>
                  </div>
                  <div style={innerContainerStyle}>
                    <input
                      type="password"
                      name="potvrdaLozinke"
                      value={passwordConfirmation}
                      onChange={handlePasswordConfirmationChange}
                      className="form-control"
                    />
                    {passwordsMatch ? null : (
                      <p className="text-danger">Šifre se ne podudaraju.</p>
                    )}
                  </div>
                </div>
              </div>
              <br/>

              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                <label>
                  Ime:
                </label>
                  </div>
                  <div style={innerContainerStyle}>
                  <input
                    type="text"
                    name="ime"
                    value={formData.ime}
                    onChange={handleChange}
                    className="form-control"
                  />
                  </div>
                </div>
              </div>
              <br/>

              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                <label>
                  Prezime:
                </label>
                  </div>
                  <div style={innerContainerStyle}>
                  <input
                    type="text"
                    name="prezime"
                    value={formData.prezime}
                    onChange={handleChange}
                    className="form-control"
                  />
                  </div>
                </div>
              </div>
              <br/>

              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                <label>
                  Datum rodjenja:
                </label>
                  </div>
                  <div style={innerContainerStyle}>
                  <input
                    type="date"
                    name="datumRodjenja"
                    value={formData.datumRodjenja}
                    onChange={handleChange}
                    className="form-control"
                  />
                  </div>
                </div>
              </div>
              <br/>

              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                <label>
                  Adresa:
                </label>
                  </div>
                  <div style={innerContainerStyle}>
                  <input
                    type="text"
                    name="adresa"
                    value={formData.adresa}
                    onChange={handleChange}
                    className="form-control"
                  />
                  </div>
                </div>
              </div>
              <br/>

              <div className="form-group">
                <div style={containerStyle}>
                  <div style={innerContainerStyle}>
                <label>
                  Slika:
                </label>
                  </div>
                  <div style={innerContainerStyle}>
                  <ImageUploader
                    onImageUpload={handleImageUpload}
                    className="form-control"
                  />
                  </div>
                </div>
              </div>
              <br/>

              {formData.tipKorisnika === "Prodavac" && (
                <div>
                  <div className="form-group">
                    <label htmlFor="postarina">Postarina:</label>
                    <input
                      type="number"
                      id="postarina"
                      name="postarina"
                      value={formData.postarina}
                      onChange={handleChange}
                      className="form-control"
                      required
                    />
                  </div>
                  <br />
                </div>
              )}
              <button type="submit" className="btn btn-primary">
                Sacuvaj izmene
              </button>
              {isSuccess && (
                <p className="text-success">Podaci su uspešno ažurirani!</p>
              )}
            </form>
          ) : (
            <p>Ucitavanje podataka...</p>
          )}
          <br />
          <br />
        </div>

        <div className="col-lg-6">
          <div className="user-details">
            <h3 style={{ color: "#FF0450" }}>Detalji</h3>
            {korisnikPodaci && (
              <div className="user-info" style={{ border: "1px solid #88d498", padding: "15px", backgroundColor: "#C1E9FF" }}>
                <p>
                  <strong>Korisničko ime:</strong>{" "}
                  {korisnikPodaci.korisnickoIme}
                </p>
                <p>
                  <strong>Email:</strong> {korisnikPodaci.email}
                </p>
                <p>
                  <strong>Ime:</strong> {korisnikPodaci.ime}
                </p>
                <p>
                  <strong>Prezime:</strong> {korisnikPodaci.prezime}
                </p>
                <p>
                  <strong>Datum rođenja:</strong> {korisnikPodaci.datumRodjenja}
                </p>
                <p>
                  <strong>Adresa:</strong> {korisnikPodaci.adresa}
                </p>
                <p>
                  <strong>Slika:                                  </strong>
                  <img
                    src={korisnikPodaci.slikaKorisnika}
                    alt="Uploaded"
                    style={{ maxWidth: "100px" }}
                  />
                </p>
              </div>
            )}
          </div>
          <br/>
          <Link to="/ulogovan-korisnik" className="mt-3">Nazad na početnu stranicu</Link>
        </div>
      </div>
    </div>
  );
};

export default Profil;
