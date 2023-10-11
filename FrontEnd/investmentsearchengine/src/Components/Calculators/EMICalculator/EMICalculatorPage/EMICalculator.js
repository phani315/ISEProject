import React, { useState, useEffect } from "react";
import lumpsumimage from "../../../../Assets/Images/Calculators/emi-calculator.png";
import EMIResults from "../EMICalculatedResults/EMIResults";
import HomeNavbar from "../../../HomePage/HomeNavbar/HomeNavbar";

function EMICalculator() {
  const [LoanAmount, setLoanAmount] = useState("");
  const [LoanTenure, setLoanTenure] = useState("");
  const [InterestRate, setInterestRate] = useState("");
  const [childLoanAmount, setchildLoanAmount] = useState("");
  const [childLoanTenure, setchildLoanTenure] = useState("");
  const [childInterestRate, setchildInterestRate] = useState("");
  const [inputErrors, setInputErrors] = useState({
    loanAmount: "",
    loanTenure: "",
    interestRate: "",
  });

  const [showEMIResults, setShowEMIResults] = useState(false);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  const handleInputChange = (event, inputNumber) => {
    const inputValue = event.target.value;
    switch (inputNumber) {
      case 1:
        setLoanAmount(inputValue);
        break;
      case 2:
        if (inputValue <= 30) {
          setLoanTenure(inputValue);
        }
        break;
      case 3:
        setInterestRate(inputValue);
        break;
      default:
        break;
    }
  };

  const validateInputs = () => {
    let valid = true;
    const errors = {
      loanAmount: "",
      loanTenure: "",
      interestRate: "",
    };

    if (LoanAmount === "") {
      errors.loanAmount = "Loan Amount is required";
      valid = false;
    }

    if (LoanTenure === "") {
      errors.loanTenure = "Loan Tenure is required";
      valid = false;
    }

    if (InterestRate === "") {
      errors.interestRate = "Interest Rate is required";
      valid = false;
    }

    setInputErrors(errors);
    return valid;
  };

  const handleReset = () => {
    setLoanAmount("");
    setLoanTenure("");
    setInterestRate("");
    setchildLoanAmount("");
    setchildLoanTenure("");
    setchildInterestRate("");
    setShowEMIResults(false);
  };

  const handleEMIResults = () => {
    if (validateInputs()) {
      setchildInterestRate(InterestRate);
      setchildLoanAmount(LoanAmount);
      setchildLoanTenure(LoanTenure);
      setShowEMIResults(true);
    }
  };

  return (
    <div className="calculator">
      <div className="calculator-body">
        <center>
          {" "}
          <h1>EMI Calculator</h1>{" "}
        </center>
        <div className="calculator-flex-container">
          <div className="calculator-flex1">
            <div className="calculatortext">
              Wish to calculate the monthly EMI of your loan? Calculate the EMI
              that you would pay every month to repay your loan using our EMI
              Calculator.
            </div>
            <hr></hr>
            <div className="calculator-function">
              <div>
                <label className="calculator-labeltext">Loan Amount *</label>
                <input
                  type="number"
                  className="calculator-input"
                  value={LoanAmount}
                  onChange={(event) => handleInputChange(event, 1)}
                />
                {inputErrors.loanAmount && (
                  <div className="error-message">{inputErrors.loanAmount}</div>
                )}
              </div>
              <div>
                <label className="calculator-labeltext">
                  Loan Tenure*(Up to 30 years)
                </label>
                <input
                  type="number"
                  className="calculator-input"
                  value={LoanTenure}
                  onChange={(event) => handleInputChange(event, 2)}
                />
                {inputErrors.loanTenure && (
                  <div className="error-message">{inputErrors.loanTenure}</div>
                )}
              </div>
              <div>
                <label className="calculator-labeltext">
                  Interest Rate(P.A) *
                </label>
                <input
                  type="number"
                  max="50"
                  className="calculator-input"
                  value={InterestRate}
                  onChange={(event) => handleInputChange(event, 3)}
                />
                {inputErrors.interestRate && (
                  <div className="error-message">
                    {inputErrors.interestRate}
                  </div>
                )}
              </div>
            </div>
            <div className="calculatorbuttons">
              <button
                className="calculator-calculatebtn"
                onClick={handleEMIResults}
              >
                Calculate My EMI Value
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
        {showEMIResults && (
          <EMIResults
            loanamount={childLoanAmount}
            loantenure={childLoanTenure}
            interestrate={childInterestRate}
          />
        )}
 <div><h2 className="center-text">About EMI Calculator</h2></div> 

<div className="calculator-description">
  <div className="content">
    <div className="section">
      <b>1) What is meant by an EMI?</b>
      <p>
        EMI stands for Equated Monthly Installments, which a borrower pays
        regularly to the lender in return for the principal and the interest
        accrued on it. The total payment amount, which is the loan amount and
        the accrued interest, is divided equally by the loan tenure to calculate
        EMI. The EMIs are to be paid until the total repayment of the loan is
        not done.
      </p>
    </div>
    <div className="section">
      <b>2) What are the benefits of EMI?</b>
      <p>
        EMI helps in reducing the financial burden from the borrower as they
        don’t have to pay the loan amount back at once. It helps the borrower to
        fulfill their luxury dreams by purchasing things in EMI as they don’t
        have to make an upfront expense. EMI is easy on the wallet and has
        flexibility as it lets us decide our EMI amount or tenure.
      </p>
    </div>
    <div className="section">
      <b>3) What is the loan tenure?</b>
      <p>
        Whenever we take loans for anything, be it a home loan, car loan,
        personal loan, we have to pay it through EMI over a specified period
        called Loan Tenure. The shorter the loan tenure, the higher the EMIs
        will be, and vice versa. So, it is advised to choose a reasonable
        duration to avoid paying high EMIs.
      </p>
    </div>
    <div className="section">
      <b>4) How is the interest rate determined in EMIs?</b>
      <p>
        The Education loan interest rate is the rate charged on the loan by the
        lender. It can be fixed or floating. If it's fixed, then the EMI amount
        will be the same every month. However, if you have opted for a floating
        interest rate, then the EMI can be affected positively or negatively
        depending upon the interest rate movement.
      </p>
    </div>
    <div className="section">
      <b>5) How is EMI calculated?</b>
      <p>
        EMI calculation is dependent on 3 things: loan amount, interest rate,
        and tenure of the repayment. The interest rate is calculated per month
        instead of per annum as the EMI is a monthly payment. Then the total
        loan amount and interest amount are divided by the tenure (number of
        months) to calculate EMI. However, we don’t need to remember any formula
        to calculate it as we have made an online EMI calculator for your
        convenience.
      </p>
    </div>
    <div className="section">
      <b>6) How to use this online EMI calculator?</b>
      <p>
        To use the EMI calculator online, you need to enter 3 things: loan
        amount, the interest rate, and the loan tenure. After entering the
        values, you will get the EMI amount that you need to pay monthly and the
        breakup of the total loan value and interest value in the pie-chart.
      </p>
    </div>
  </div>
</div>

      </div>
    </div>
  );
}

export default EMICalculator;
