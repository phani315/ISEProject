import React from "react";
import "./DisplayWatchList.css";
import Card from "./Card";
import backicon from "../../Assets/Images/ComparePage/ComparePageHeader/BackSymbol.svg";
import { useNavigate } from "react-router";
import BackComponent from '../Compare/ComparePageHeader/ComparePageHeader'
function DisplayWatchList() {
  const navigate = useNavigate();
  return (
    <div className="display">
      <div className="mainContainer">
        <div className="contentContainer">
          <div className="headerContainer">
            <div className="headerLeftContainer">
               <BackComponent></BackComponent>
              <div className="headContainer">Your watchlist</div>
              <div className="watchlistText">
                You have added the following components to your watchlist. Your
                watchlist is updated every day.
              </div>
            </div>
          </div>
          <div className="WatchlistCardComponent">
            <Card />
          </div>
        </div>
      </div>
    </div>
  );
}

export default DisplayWatchList;
