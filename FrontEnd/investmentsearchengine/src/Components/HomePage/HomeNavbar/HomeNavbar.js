import React, { useState } from "react";
import { Link, Route, Routes } from "react-router-dom";
import KaniniLogo from "../../../Assets/Images/Navbar/KaniniLogo.svg";
import { AiOutlineMenu } from "react-icons/ai";
import { MdCancel } from "react-icons/md";
import "../HomeNavbar/HomeNavbar.css";
import Home from "../Home/Home";
import UserProfile from "../../../Assets/Images/Home/UserProfile.png";
import AboutUs from "../AboutUs/AboutUs";
import Calculators from "../../CalculatorPage/CalculatorPage";
import EMICalculator from "../../Calculators/EMICalculator/EMICalculatorPage/EMICalculator";
import InvestmentPlanner from "../../Calculators/InvestmentPlanner/InvestmentPlannerPage/Investmentplanner";
import LumpsumCalcuator from "../../Calculators/LumpsumCalcuator/LumpsumCalculatorPage/LumpsumCalculator";
import { useLocation } from "react-router-dom";
function HomeNavbar() {
  const location = useLocation();

  const [toggleDropDown, setDropDown] = useState(false);
  return (
    <div className="Navbar">
      <header className="NavbarHeader">
        <div className="navbar">
          <div className="navContainer KaniniLogoContainer">
            <img src={KaniniLogo} className="KaniniLogo" />
          </div>
          <div className="navContainer">
          <ul className="links">
  <li className={`navButton ${location.pathname === '/' ? 'active' : ''}`}>
    <Link className="link" to="/">
      Home
    </Link>
  </li>
  <li className={`navButton ${location.pathname === '/aboutus' ? 'active' : ''}`}>
    <Link className="link" to="/aboutus">
      About us
    </Link>
  </li>
  <li className={`navButton ${location.pathname === '/calculator' ? 'active' : ''}`}>
    <Link className="link" to="/calculator">
      Calculator
    </Link>
  </li>
</ul>

          </div>
          <div className="navContainer loginContainer">
            <Link to="/Authentication" className="action_btn link">
              <img src={UserProfile} className="UserProfileIcon" />
              Login/Register
            </Link>
          </div>
          <div
            className="toggle_btn"
            onClick={() => {
              setDropDown(!toggleDropDown);
            }}
          >
            {toggleDropDown ? (
              <MdCancel className="menuOption" />
            ) : (
              <AiOutlineMenu className="menuOption" />
            )}
          </div>
        </div>
        <div
          className={toggleDropDown ? "dropdown_menu open" : "dropdown_menu"}
        >
          <li className="navButton">
            <Link className="link dropdownLink" to="/">
              Home
            </Link>
          </li>
          <li className="navButton">
            <Link className="link dropdownLink" to="/aboutus">
              About us
            </Link>
          </li>
          <li className="navButton">
            <Link className="link dropdownLink" to="/calculator">
              Calculator
            </Link>
          </li>
          <li>
            <Link to="/Authentication" className="link dropdownLink">
              Login/Register
            </Link>
          </li>
        </div>
      </header>
      <div className="navbarBottomBorderContainer">
        <hr className="navbarBottomBorder" />
      </div>
      <div>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/aboutus" element={<AboutUs />} />
          <Route path="/calculator" element={<Calculators />} />
          <Route path="/EmiCalculator" element={<EMICalculator />} />
          <Route path="/InvestmentPlanner" element={<InvestmentPlanner />} />
          <Route path="/LumpsumCalculator" element={<LumpsumCalcuator />} />
        </Routes>
      </div>
      <div className="footer">
        <div className="footerCopyRightContainer">
          <span>&#169; 2023 Kanini. All Rights Reserved.</span>
        </div>
        <div className="footerTermsContainer">
          <span>Terms & Conditions</span>
          <span>Privacy</span>
          <span>Cookie Settings</span>
        </div>
      </div>
    </div>
  );
}

export default HomeNavbar;
