import React from "react";
import "../CompanyEssentials/CompanyEssentials.css";
import infoSymbol from "../../Assets/Images/StocksDashboard/infoSymbol.svg";

function CompanyEssentials(props) {
  return (
    <div className="CompanyEssentials">
      <div className="EssentialsHeader">
        <label className="companyEsentialsHeader">Company Essentials</label>
      </div>
      <hr className="horizontalRule" />
      <div className="esentialInfo">
        <span className="esentialValueTitle esentialValue">MARKET CAP</span>
        <span className="esentialValue">
          <span>&#8377;</span>
          {props.essentials.marketCap} Cr.
        </span>
      </div>
      <div className="evenEsential">
        <span className="esentialValueTitle esentialValue">
          ENTERPRISES VALUE
        </span>
        <span className="esentialValue">
          <span>&#8377;</span> {props.essentials.enterpriceValue} Cr.
        </span>
      </div>
      <div className="esentialInfo ">
        <span className="esentialValueTitle esentialValue">NO. OF SHARES</span>
        <span className="esentialValue">
          <span>&#8377;</span> {props.essentials.noOfShares} Cr.
        </span>
      </div>
      <div className="evenEsential ">
        <span className="esentialValueTitle esentialValue">DIV YIELD</span>
        <span className="esentialValue">{props.essentials.divYield}%</span>
      </div>
      <div className="esentialInfo ">
        <span className="esentialValueTitle esentialValue">CASH</span>
        <span className="esentialValue">
          <span>&#8377;</span> {props.essentials.cash} Cr.
        </span>
      </div>
      <div className="evenEsential ">
        <span className="esentialValueTitle esentialValue">
          PROMOTER HOLDING
        </span>
        <span className="esentialValue">
          {props.essentials.promoterHolding}%
        </span>
      </div>
    </div>
  );
}

export default CompanyEssentials;
