import React from "react";
import "../SwotAnalysis/SwotAnalysis.css";
function SwotAnalysis(props) {
  return (
    <div className="SwotAnalysis">
      <div className="Swot">
        <div className="SwotCount">
          <span className="SwotCountValue">{props.swot.strengthValue}</span>
        </div>
        <div className="SwotDescription">
          <span className="SwotTitle">Strengths</span>
          <span className="SwotInfo">{props.swot.strengthDescription}</span>
        </div>
      </div>
      <div className="Swot">
        <div className="SwotCount">
          <span className="SwotCountValue">{props.swot.weaknessValue}</span>
        </div>
        <div className="SwotDescription">
          <span className="SwotTitle">Weaknesses</span>
          <span className="SwotInfo">{props.swot.weaknessDescription}</span>
        </div>
      </div>
      <div className="Swot">
        <div className="SwotCount">
          <span className="SwotCountValue">{props.swot.oppurtunityValue}</span>
        </div>
        <div className="SwotDescription">
          <span className="SwotTitle">Opportunities</span>
          <span className="SwotInfo">{props.swot.oppurtunityDescription} </span>
        </div>
      </div>
      <div className="Swot">
        <div className="SwotCount">
          <span className="SwotCountValue">{props.swot.threatValue}</span>
        </div>
        <div className="SwotDescription">
          <span className="SwotTitle">Threats</span>
          <span className="SwotInfo">{props.swot.threatDescription}</span>
        </div>
      </div>
    </div>
  );
}
export default SwotAnalysis;
