import React, { useState, useEffect } from "react";
import "./LumpsumCalculator.css";
import LumpsumResults from "../LumpsumCalculatedResults/lumpsumresults";
import lumpsumimage from "../../../../Assets/Images/Calculators/lum-sum-calculator.png";

function LumpsumCalcuator() {
  const [InvestmentAmount, setInvestmentAmount] = useState("");
  const [ExpectedRateOfReturn, setExpectedRateOfReturn] = useState("");
  const [Tenure, setTenure] = useState("");
  const [ChildInvestmentAmount, setChildInvestmentAmount] = useState("");
  const [ChildExpectedRateOfReturn, setChildExpectedRateOfReturn] =useState("");
  const [ChildTenure, setChildTenure] = useState("");
  const [showLumpsumResults, setshowLumpsumResults] = useState(false);
  const [inputErrors, setInputErrors] = useState({
    investmentAmount: "",
    expectedRateOfReturn: "",
    tenure: "",
  });
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  const handleInputChange = (event, inputNumber) => {
    const inputValue = event.target.value;
    switch (inputNumber) {
      case 1:
        setInvestmentAmount(inputValue);
        break;
      case 2:
        if (inputValue <= 50) {
          setExpectedRateOfReturn(inputValue);
        }
        break;
      case 3:
        if (inputValue <= 50) {
          setTenure(inputValue);
        }
        break;
      default:
        break;
    }
  };

  const handleLumpsumResults = () => {
    if (validateInputs()) {
      setChildExpectedRateOfReturn(ExpectedRateOfReturn);
      setChildInvestmentAmount(InvestmentAmount);
      setChildTenure(Tenure);
      setshowLumpsumResults(true);
    }
  };
  const handleReset = () => {
    setInvestmentAmount("");
    setExpectedRateOfReturn("");
    setTenure("");
    setChildInvestmentAmount("");
    setChildExpectedRateOfReturn("");
    setChildTenure("");
    setshowLumpsumResults(false);
  };

  const validateInputs = () => {
    let valid = true;
    const errors = {
      investmentAmount: "",
      expectedRateOfReturn: "",
      tenure: "",
    };

    if (InvestmentAmount === "") {
      errors.investmentAmount = "Investment Amount is required";
      valid = false;
    }
    if (ExpectedRateOfReturn === "") {
      errors.expectedRateOfReturn = "Expected Rate of Return is required";
      valid = false;
    }
    if (Tenure === "") {
      errors.tenure = "Tenure is required";
      valid = false;
    }
    setInputErrors(errors);
    return valid;
  };

  return (
    <div className="calculator">
      <div className="calculator-body">
        <center>
          {" "}
          <h1>Lumpsum Calculator</h1>{" "}
        </center>
        <div className="calculator-flex-container">
          <div className="calculator-flex1">
            <div className="calculatortext">
              Thinking of making a Lumpsum investment? Calculate the future
              value of your wealth using our Lumpsum Calculator.
            </div>
            <hr></hr>
            <div className="calculator-function">
              <div>
                <label className="calculator-labeltext">
                  Investment Amount
                </label>
                <input
                  type="number"
                  className="calculator-input"
                  value={InvestmentAmount}
                  onChange={(event) => handleInputChange(event, 1)}
                />
                {inputErrors.investmentAmount && (
                  <div className="error-message">
                    {inputErrors.investmentAmount}
                  </div>
                )}
              </div>
              <div>
                <label className="calculator-labeltext">
                  Expected rate of return (P.A)
                </label>
                <input
                  type="number"
                  className="calculator-input"
                  value={ExpectedRateOfReturn}
                  onChange={(event) => handleInputChange(event, 2)}
                />
                {inputErrors.expectedRateOfReturn && (
                  <div className="error-message">
                    {inputErrors.expectedRateOfReturn}
                  </div>
                )}
              </div>

              <div>
                <label className="calculator-labeltext">
                  Tenure (in years) (Up to 50 years)
                </label>
                <input
                  type="number"
                  max="50"
                  className="calculator-input"
                  value={Tenure}
                  onChange={(event) => handleInputChange(event, 3)}
                />
                {inputErrors.tenure && (
                  <div className="error-message">{inputErrors.tenure}</div>
                )}
              </div>
            </div>

            <div className="calculatorbuttons">
              <button
                className="calculator-calculatebtn"
                onClick={handleLumpsumResults}
              >
                Plan my Future Value
              </button>
              <button className="calculator-resetbtn" onClick={handleReset}>
                Reset
              </button>
            </div>
          </div>

          <div className="calculator-flex2">
            <img
              className="calculator-image"
              src={lumpsumimage}
              alt="Lumpsum"
            />
          </div>
        </div>

        {/* Display the LumpsumResultsComponent if showLumpsumResults is true */}
        {showLumpsumResults && (
          <LumpsumResults
            investmentAmount={ChildInvestmentAmount}
            expectedRateOfReturn={ChildExpectedRateOfReturn}
            tenure={ChildTenure}
          />
        )}
        <div className="calculator-heading">    
                <h2 className="center-text">About Lumpsum Calculator</h2>
</div>

<div className="calculator-description">
  <div className="content">
    <div className="section">
      <b>1) What is a Lumpsum Investment?</b>
      <p>
        Lumpsum investment or one-time investment is a style of investment in
        which you invest once (lumpsum) and allow your invested money to
        generate compounding returns over a given time frame.
      </p>
    </div>
    <div className="section">
      <b>2) What Is Lumpsum Calculator?</b>
      <p>
        With Lumpsum calculator you can calculate the maturity value of your
        investment. In other words, the Lumpsum Calculator tells the future
        value of your investment made today at a certain rate of interest. For
        example: If you invest 1 lakh rupees for 60 years at 15% rate of
        interest then according to lumpsum calculator, the future value of your
        investments will be mindboggling 43.8 cr. after 60 years.
      </p>
    </div>
    <div className="section">
      <b>3) How does this Lumpsum Calculator work?</b>
      <p>
        Our lumpsum calculator is so convenient to use that even a layman can
        use it. In our Lumpsum Calculator, you need to just enter the required
        inputs such as the amount you are willing to invest, the time period (in
        years) you are willing to stay invested and, the expected rate of return
        per annum that you think the investment will generate. After entering
        the required variables, the calculator will give you the future value
        of your investments. The formula that we have used in this Lumpsum
        Calculator is: Value = Investment*(1+R)N
      </p>
    </div>
    <div className="section">
      <b>4) When should one prefer Lumpsum Investment?</b>
      <p>
        Ideally any investment (whether lumpsum or SIP) should be done keeping
        in mind various things like current income, risk profile, age, tax
        constraints, liquidity needs, time frame and certain other unique
        constraints. Lumpsum investment is preferred when one has a large amount
        of surplus funds and more importantly if he/she thinks that the market
        has majorly corrected or it won’t fall just after making the investment.
        Lumpsum investment done over a longer period helps generate compounding
        rate of returns.
      </p>
    </div>
    <div className="section">
      <b>5) What’s the difference between Lumpsum and SIP?</b>
      <p>
        In lumpsum investment, one needs to invest only once whereas, in SIP or
        Systematic Investment Plan one invests a fixed amount periodically. In
        the lumpsum investment style, the market condition plays a huge role
        because if the market makes a major correction after your investment,
        then it might take a few years to reach your original investment amount.
        Whereas, in SIP or systematic investment style one need not worry about
        timing the market as investment is made during both ups and downs of the
        market. Therefore, the return generated is a weighted average return.
      </p>
    </div>
    <div className="section">
      <b>6) Where can I park my funds for Lumpsum investment?</b>
      <p>
        For lumpsum investment, one can choose various instruments like Mutual
        Funds, Equity Shares, Exchange Traded Funds, Liquid Funds, Bonds, Fixed
        Deposits, etc. But again, we think that you should select these
        instruments for lumpsum investment only after considering your risk
        profile, financial goals, liquidity needs, etc.
      </p>
    </div>
  </div>
</div>

      </div>
    </div>
  );
}

export default LumpsumCalcuator;
