import React from "react";
import "../HighLowAverages/HighLowAverages.css";
import TodaysHigh from "../../Assets/Images/StocksDashboard/TodaysHigh.svg";
import TodaysLow from "../../Assets/Images/StocksDashboard/TodaysLow.svg";
import YearHigh from "../../Assets/Images/StocksDashboard/WeekHigh.svg";
import YearLow from "../../Assets/Images/StocksDashboard/WeekLow.svg";

function HighLowAverages(props) {
  return (
    <div className="HighLowAverages">
      <div className="highLowCard">
        <div className="todayHighimageBackground highBackground">
          <img src={TodaysHigh} className="highLowImage" />
        </div>
        <div className="highLowInfo">
          <span className="highLowLabel">Today’s High</span>
          <span className="highLowValue">
            <span>&#8377;</span> <span>{props.averages.todayHigh}</span>
          </span>
        </div>
      </div>
      <div className="highLowCard">
        <div className="todayHighimageBackground lowBackground">
          <img src={TodaysLow} className="highLowImage" />
        </div>
        <div className="highLowInfo">
          <span className="highLowLabel">Today’s Low</span>
          <span className="highLowValue">
            <span>&#8377;</span>
            <span>{props.averages.todayLow}</span>
          </span>
        </div>
      </div>
      <div className="highLowCard">
        <div className="todayHighimageBackground yearHighBackground">
          <img src={YearHigh} className="highLowImage" />
        </div>
        <div className="highLowInfo">
          <span className="highLowLabel">52 Week High</span>
          <span className="highLowValue">
            <span>&#8377;</span> <span>{props.averages.yearHigh}</span>
          </span>
        </div>
      </div>
      <div className="highLowCard">
        <div className="todayHighimageBackground yearLowBackground">
          <img src={YearLow} className="highLowImage" />
        </div>
        <div className="highLowInfo">
          <span className="highLowLabel">52 Week Low</span>
          <span className="highLowValue">
            <span>&#8377;</span> <span>{props.averages.yearLow}</span>
          </span>
        </div>
      </div>
    </div>
  );
}
export default HighLowAverages;
