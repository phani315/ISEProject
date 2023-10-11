import React from "react";
import "bootstrap-icons/font/bootstrap-icons.css";
import { Route, Routes } from "react-router-dom";
import { Navigate,useNavigate } from "react-router-dom";
import "./AuthenticationDesign.css";
import {FaHome} from 'react-icons/fa';
import { Link } from "react-router-dom";
import AuthenticationTopImage from "../../../Assets/Images/Authentication/AuthenticationMessageTop.png";
import AuthenticationBottomImage from "../../../Assets/Images/Authentication/AuthenticationMessageBottom.png";
import AuthenticationSignInBottomImage from "../../../Assets/Images/Authentication/AuthenticationSignInBottom.png";
import KaniniLogo from "../../../Assets/Images/Authentication/Kanini-Logo.png";
import SignIn from "../SignIn/Signin";
import SignUp from "../SignUp/Signup";

function Authentication() {
    return (
    <div className="Authentication">
    <div className="HomeNavigation">
        <Link to="/">
            <FaHome className="Homenavigateicon"/>
        </Link>
        </div>
      <div className="InvestmentSearchEngineMessage">
        <div className="AuthTopRightImageContainer">
          <img
            src={AuthenticationTopImage}
            className="AuthenticationTopImage"
          />
        </div>
        <div className="AuthCenterMessage">
          <div className="AuthCenterContent">
            <div className="AuthQuoteContainer">
              <i className="bi bi-quote"></i>
            </div>
            <div>
              <span className="AuthQuoteTitle">
                Experience Seamless Stock <br></br> Search Here
              </span>
            </div>
            <div>
              <span className="AuthQuoteSubTitle">Kanini Ticker Platform</span>
            </div>
          </div>
        </div>
        <div>
          <img
            src={AuthenticationBottomImage}
            className="AuthenticationBottomImage"
          />
        </div>
      </div>
      <div className="AuthenticationCredentials">
        <div className="KaniniLogoContainer">
          <img src={KaniniLogo} className="KaniniLogo" />
        </div>
        <div>
          <Routes>
            <Route path="/" element={<SignIn />} />
            <Route path="/signup" element={<SignUp />} />
          </Routes>
        </div>
        <div>
          <img
            src={AuthenticationSignInBottomImage}
            className="AuthenticationSignInBottomImage"
          />
        </div>
      </div>
    </div>
  );
}

export default Authentication;