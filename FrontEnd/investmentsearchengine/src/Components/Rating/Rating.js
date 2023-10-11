import React from "react";
import "../Rating/Rating.css";

function Rating(props) {
  if (!props.rating) {
    return <div>Loading...</div>;
  }
  return (
    <div className="Rating">
      <div className="ratingHeader">
        <div className=" ratingTitle">Finstar</div>
        <div className="ratingStars">
          <i data-star={props.rating.totalRating} className="stars"></i>
        </div>
        <div>
          {props.rating.totalRating} ({props.rating.totalReviewCount})
        </div>
      </div>
      <div className="ratingAverages">
        <div className="stableExepensive">
          <fieldset className="ratingFieldset">
            <legend
              style={{
                background:
                  props.rating.ownerShipRate > 2 ? "#b9dd4e" : "#ed2939",
              }}
            >
              Stable
            </legend>
            <div className="ratingDisplay">
              <div>
                <span className="averageTitle">Ownership</span>
              </div>
              <div>
                <span className="rated" id="starRatingAverages">
                  &#9733;
                </span>
                <span>
                  <span className="averageRatingCount">
                    {props.rating.ownerShipRate}
                  </span>
                  <span className="averageValue">
                    ({props.rating.ownerShipReviewCount})
                  </span>
                </span>
              </div>
            </div>
          </fieldset>
          <fieldset className="ratingFieldset">
            <legend
              style={{
                background:
                  props.rating.valuationRate > 2 ? "#b9dd4e" : "#ed2939",
              }}
            >
              Expensive
            </legend>
            <div className="ratingDisplay">
              <div>
                <span className="averageTitle">Valuation</span>
              </div>
              <div>
                <span className="rated" id="starRatingAverages">
                  &#9733;
                </span>
                <span>
                  <span className="averageRatingCount">
                    {props.rating.valuationRate}
                  </span>
                  <span className="averageValue">
                    ({props.rating.valuationReviewCount})
                  </span>
                </span>
              </div>
            </div>
          </fieldset>
        </div>
        <div className="optimalAverages">
          <fieldset className="ratingFieldset">
            <legend
              style={{
                background:
                  props.rating.efficiencyRate > 2 ? "#b9dd4e" : "#ed2939",
              }}
            >
              Optimal
            </legend>
            <div className="ratingDisplay">
              <div>
                <span className="averageTitle">Efficiency</span>
              </div>
              <div>
                <span className="rated" id="starRatingAverages">
                  &#9733;
                </span>
                <span>
                  <span className="averageRatingCount">
                    {props.rating.efficiencyRate}
                  </span>
                  <span className="averageValue">
                    ({props.rating.efficienncyReviewCount})
                  </span>
                </span>
              </div>
            </div>
          </fieldset>
          <fieldset className="ratingFieldset">
            <legend
              style={{
                background:
                  props.rating.financialRate > 2 ? "#b9dd4e" : "#ed2939",
              }}
            >
              Average
            </legend>
            <div className="ratingDisplay">
              <div>
                <span className="averageTitle">Financials</span>
              </div>
              <div>
                <span className="rated" id="starRatingAverages">
                  &#9733;
                </span>
                <span>
                  <span className="averageRatingCount">
                    {props.rating.financialRate}
                  </span>
                  <span className="averageValue">
                    ({props.rating.financialReviewCount})
                  </span>
                </span>
              </div>
            </div>
          </fieldset>
        </div>
      </div>
    </div>
  );
}

export default Rating;
