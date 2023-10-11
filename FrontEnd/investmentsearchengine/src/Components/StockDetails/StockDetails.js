import React, { useEffect, useState } from "react";
import "../StockDetails/StockDetails.css";
import CompanyTitle from "../CompanyTitle/CompanyTitle";
import HighLowAverages from "../HighLowAverages/HighLowAverages";
import SwotAnalysis from "../SwotAnalysis/SwotAnalysis";
import Rating from "../Rating/Rating";
import CompanyEssentials from "../CompanyEssentials/CompanyEssentials";
import { useParams } from "react-router-dom";

function StockDetails() {
  const { id } = useParams();
  const [companyTitle, setCompanyTitle] = useState([]);
  const [currentStockPrice, setCurrentStockPrice] = useState([]);
  const [companyAverages, setCompanyAverages] = useState([]);
  const [swotAnalysis, setSwotAnalysis] = useState([]);
  const [companyEssentilas, setCompanyEssentilas] = useState([]);
  const [companyRating, setCompanyRating] = useState([]);

  useEffect(() => {
    var token = localStorage.getItem("token");
    fetch(process.env.REACT_APP_COMPANYDETAILS_API+"CompanyDetails/" + id, {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then(async (response) => {
        if (response.ok) {
          const data = await response.json();
          setCompanyTitle(data);
        }
      })
      .catch((error) => {
        console.error(error);
      });

    fetch(
      process.env.REACT_APP_STOCKPRICE_API+"StockPrice/GetStockDetailsAveragesCompanyID?companyId=" +
        id,
      {
        method: "POST",
        headers: {
          accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    )
      .then(async (response) => {
        if (response.ok) {
          const data = await response.json();
          setCompanyAverages(data);
        }
      })
      .catch((error) => {
        console.error(error);
      });

    fetch(
      process.env.REACT_APP_STOCKPRICE_API+"StockPrice/GetCurrentStockDetailsByCompanyID?companyId=" +
        id,
      {
        method: "POST",
        headers: {
          accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    )
      .then(async (response) => {
        if (response.ok) {
          const data = await response.json();
          setCurrentStockPrice(data);
        }
      })
      .catch((error) => {
        console.error(error);
      });

    fetch(process.env.REACT_APP_SWOTANALYSIS_API+"SWOT/GetSwotByCompanyID?companyId=" + id, {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then(async (response) => {
        if (response.ok) {
          const data = await response.json();
          setSwotAnalysis(data);
        }
      })
      .catch((error) => {
        console.error(error);
      });

    fetch(
      process.env.REACT_APP_COMPANYESSENTIALS_API+"CompanyEssentials/GetEssentialsByCompany?id=" +id,
      {
        method: "POST",
        headers: {
          accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    )
      .then(async (response) => {
        if (response.ok) {
          const data = await response.json();
          setCompanyEssentilas(data);
        }
      })
      .catch((error) => {
        console.error(error);
      });

    fetch(process.env.REACT_APP_FINSTARRATING_API+"Finstar/GetFinstarDetails?id=" + id, {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then(async (response) => {
        if (response.ok) {
          const data = await response.json();
          setCompanyRating(data);
        }
      })
      .catch((error) => {
        console.error(error);
      });
  }, [id]);
  return (
    <div className="StockDetails">
      <div className="StockDetailsCompanyTitle">
        <CompanyTitle title={companyTitle} stockPrice={currentStockPrice} />
      </div>
      <div className="StockDetailsAverages">
        <HighLowAverages averages={companyAverages} />
      </div>
      <div className="StockDetailsSwot">
        <SwotAnalysis swot={swotAnalysis} />
      </div>
      <div className="StockDetailsCompanyEssentials">
        <CompanyEssentials essentials={companyEssentilas} />
      </div>
      <div className="StockDetailsRating">
        <Rating rating={companyRating} />
      </div>
    </div>
  );
}

export default StockDetails;
