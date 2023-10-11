import React from "react";
import Navbar from "../../Navbar/Navbar";
import backicon from "../../../Assets/Images/ComparePage/ComparePageHeader/BackSymbol.svg";
import { useNavigate } from "react-router";
import "./ComparePageHeader.css";

function CompareHeader({ id }) {
  const navigate = useNavigate();

  return (
    <header className="comparepageheader">
      <div className="comparepage-back">
        <div
          className="backButton"
          onClick={(event) => {
            event.preventDefault();
            navigate(-1);
          }}
        >
          <div>
            <img src={backicon} alt="back icon" className="backSymbol" />
            <span className="backText">Back</span>
          </div>
        </div>
      </div>
    </header>
  );
}

export default CompareHeader;
