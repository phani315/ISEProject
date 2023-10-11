import React from "react";
import "../Dashboard/Dashboard.css";
import Navbar from "../Navbar/Navbar";
import LandingPage from "../LandingPage/LandingPage";
import { Routes, Route } from "react-router";
import StockDetails from "../StockDetails/StockDetails";
import Compare from "../Compare/ComparisionTable/Compare";
import DisplayWatchList from "../Watchlist/DisplayWatchList";

function Dashboard() {
  return (
    <div>
      <div className="Dashboard">
        <div className="DashboardNavbar">
          <Navbar></Navbar>
        </div>
        <div className="DashboardContainer">
          <Routes>
            <Route path="/" element={<LandingPage />} />
            <Route path="/stockdetails/:id" element={<StockDetails />} />
            <Route path="/compare/:id" element={<Compare />} />
            <Route path="/watchlist" element={<DisplayWatchList />} />
          </Routes>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;
