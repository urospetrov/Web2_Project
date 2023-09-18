import React from "react";
import {Link} from 'react-router-dom';
//test commit

function PocetnaStranica(){
    const containerStyle = {
        display: 'inline', // or 'inline-block' or 'inline-flex' depending on your needs
    };

    const elementStyle = {
        marginRight: "25px", // Add spacing between inline elements if needed
    };
    
    return (
        <div className="container text-center mt-5">
            <h1 className="mb-4">Web Shop</h1>
            <div style={containerStyle} >
                <Link to="/registracija" className="btn btn-primary mr-6" style={elementStyle}>Registracija</Link>
                <Link to="/logovanje" className="btn btn-primary inline-element" style={elementStyle}>Login</Link>
            </div>
        </div>
    );
}

export default PocetnaStranica;