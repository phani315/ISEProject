import React, { useEffect } from "react";
import "../CompanyTitle/CompanyTitle.css";
import BackSymbol from "../../Assets/Images/ComparePage/ComparePageHeader/BackSymbol.svg";
import { Link, useNavigate } from "react-router-dom";
import { FaRegEye } from "react-icons/fa";
import { MdCompareArrows } from "react-icons/md";
import { BiSolidUpArrow } from "react-icons/bi";
import { BiSolidDownArrow } from "react-icons/bi";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function CompanyTitle(props) {
  const updatedStockPrice = props.stockPrice.updatedStockPrice;
  const date = new Date();
  const hour = date.getHours();
  const minute = date.getMinutes();
  const navigate = useNavigate();
  const { id } = useParams();
  const AddToWatchlist = async (companyId) => {
    fetch("http://localhost:5065/api/WatchList/AddingToWatchList", {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        userId: localStorage.getItem("userId"),
        companyId: companyId,
      }),
    })
      .then(async (response) => {
        const data = await response.json();
        if (response.status === 200) {
          toast.success("Company successfully added to watchlist!");
        } else {
          if (data && data.errorNumber === 400) {
            toast.warn(data.errorMessage);
          } else {
            toast.error("An error occurred.");
          }
        }
      })
      .catch((err) => {
        toast.error("Error! Something went wrong.");
        console.log("Error adding company to wishlist:");
      });
  };

  return (
    <div className="CompanyTitle">
      <div>
        <div
          className="backButton"
          onClick={() => {
            navigate("/dashboard/");
          }}
        >
          <div>
            <img src={BackSymbol} className="backSymbol" />
            <span className="backText">Back</span>
          </div>
        </div>
        <div className="companyName">
          <div>
            <span className="companyTitle">{props.title.companyName}</span>
          </div>
          <Link
            onClick={() => {
              AddToWatchlist(id);
            }}
            className="watchlist"
          >
            <FaRegEye />
          </Link>
          <Link to={`/dashboard/compare/${id}`} className="addToCart">
            <MdCompareArrows />
          </Link>
        </div>
        <div className="companyCodes">
          <div className="companyLabelCode">
            <label>NSE :</label>
            <span className="stockExchnageCode"> {props.title.nse}</span>
          </div>
          <div className="companyLabelCode">
            <label>BSE :</label>
            <span className="stockExchnageCode">{props.title.bse}</span>
          </div>
          <div className="companyLabelCode">
            <label>SECTOR :</label>
            <span className="stockExchnageSector">{props.title.sector}</span>
          </div>
        </div>
      </div>
      <div className="upStockButton">
        <div
          className="stockChangeButton"
          style={{
            background: updatedStockPrice >= 0 ? "#b9dd4e" : "#ed2939",
          }}
        >
          <>
            {updatedStockPrice >= 0 ? (
              <BiSolidUpArrow className="UpStockButtonImage" />
            ) : (
              <BiSolidDownArrow className="UpStockButtonImage" />
            )}
          </>
          <span>{props.stockPrice.currentStockPrice}</span>
        </div>
        <div className="stockChangeValue">
          <span
            className="stockIncrementPercentrage"
            style={{
              color:
                updatedStockPrice >= 0
                  ? "var(--dark-grey, #161616)"
                  : "#ed2939",
            }}
          >
            {updatedStockPrice >= 0 ? "+" : ""}
            {props.stockPrice.updatedStockPrice} (
            {props.stockPrice.updatedStockPercent}%)
          </span>
          <span>
            <span className="nselabel">NSE: </span>
            <span className="nseTimingLabel">
              Today {hour}:{minute}
            </span>
          </span>
        </div>
      </div>
    </div>
  );
}
export default CompanyTitle;
