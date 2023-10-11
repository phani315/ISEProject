import React from "react";
import "./CalculatorPage.css";
import { Link, useNavigate } from "react-router-dom";
import HomeNavbar from "../HomePage/HomeNavbar/HomeNavbar";

function Calculators() {
  const navigate = useNavigate();
  return (
    <div className="Calulator-body">
      <section className="calculator" id="calculator">
        <div className="calculator-heading">
          <h3>Financial Calculators</h3>
        </div>
        <div className="calculator-container">
          <div className="calculator-cards">
            <div className="cal-image-section cal-img-one"></div>
            <div className="calculators-card-content">
              <h3>Lumpsum Calculator</h3>
              <p>
                Thinking of making a lumpsum investment?Calculate the future
                value of your wealth using our Lumpsum Calculator.
              </p>
              <Link to="/LumpsumCalculator" className="launch-calculator">
                Launch Calculator
              </Link>
            </div>
          </div>
          <div className="calculator-cards">
            <div className="cal-image-section cal-img-two"></div>
            <div className="calculators-card-content">
              <h3>EMI Calculator</h3>
              <p>
                Wish to calculate the monthly EMI of your loan? Calculate the
                EMI that you would pay every month to repay your loan using our
                EMI Calculator.
              </p>
              <Link to="/EmiCalculator" className="launch-calculator">
                Launch Calculator
              </Link>
            </div>
          </div>
          <div className="calculator-cards">
            <div className="cal-image-section cal-img-three"></div>
            <div className="calculators-card-content">
              <h3>Investment Planner</h3>
              <p>
                What % of your salary you ivest in Equity? Calculate the
                investment amount using our Ideal investment Calculator.
              </p>
              <Link to="/InvestmentPlanner" className="launch-calculator">
                Launch Calculator
              </Link>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}
export default Calculators;
