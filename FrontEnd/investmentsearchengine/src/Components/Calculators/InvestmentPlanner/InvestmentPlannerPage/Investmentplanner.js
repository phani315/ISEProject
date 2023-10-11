import React, { useState, useEffect } from "react";
import lumpsumimage from "../../../../Assets/Images/Calculators/time-investment.png";
import InvestmentPlannerResults from "../InvestmentPlannerResults/investmentplannerresults";
function InvestmentPlanner() {
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  const [monthlySalary, setMonthlySalary] = useState("");
  const [age, setAge] = useState("");
  const [monthlySavings, setMonthlySavings] = useState("");

  const [childmonthlySalary, setchildMonthlySalary] = useState("");
  const [childage, setchildAge] = useState("");
  const [childmonthlySavings, setchildMonthlySavings] = useState("");
  const [inputErrors, setInputErrors] = useState({
    monthlySalary: "",
    age: "",
    monthlySavings: "",
  });

  const [showResults, setShowResults] = useState(false);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  const handleInputChange = (event, inputName) => {
    const inputValue = event.target.value;
    switch (inputName) {
      case "monthlySalary":
        setMonthlySalary(inputValue);
        break;
      case "age":
        setAge(inputValue);
        break;
      case "monthlySavings":
        setMonthlySavings(inputValue);
        break;
      default:
        break;
    }
  };
  const validateInputs = () => {
    let valid = true;
    const errors = {
      monthlySalary: "",
      age: "",
      monthlySavings: "",
    };

    if (monthlySalary === "") {
      errors.monthlySalary = "Monthly Salary is required";
      valid = false;
    }

    if (age === "") {
      errors.age = "Age is required";
      valid = false;
    }

    if (monthlySavings === "") {
      errors.monthlySavings = "Monthly Savings is required";
      valid = false;
    }

    setInputErrors(errors);
    return valid;
  };

  const handleReset = () => {
    setMonthlySalary("");
    setAge("");
    setMonthlySavings("");
    setchildMonthlySalary("");
    setchildAge("");
    setchildMonthlySavings("");
    setShowResults(false);
  };

  const handleCalculate = () => {
    if (validateInputs()) {
      setchildMonthlySalary(monthlySalary);
      setchildAge(age);
      setchildMonthlySavings(monthlySavings);
      setShowResults(true);
    }
  };

  return (
    <div className="calculator">
      <div className="calculator-body">
        <center>
          <h1>Investment Planner</h1>
        </center>
        <div className="calculator-flex-container">
          <div className="calculator-flex1">
            <div className="calculatortext">
              What % of your salary should you invest in Equity? Calculate the
              investment amount using our Ideal Investment Calculator.
            </div>
            <hr></hr>
            <div className="calculator-function">
              <div>
                <label className="calculator-labeltext">Monthly Salary *</label>
                <input
                  type="number"
                  className="calculator-input"
                  value={monthlySalary}
                  onChange={(event) =>
                    handleInputChange(event, "monthlySalary")
                  }
                />
                {inputErrors.monthlySalary && (
                  <div className="error-message">
                    {inputErrors.monthlySalary}
                  </div>
                )}
              </div>
              <div>
                <label className="calculator-labeltext">Age *</label>
                <input
                  type="number"
                  className="calculator-input"
                  value={age}
                  onChange={(event) => handleInputChange(event, "age")}
                />
                {inputErrors.age && (
                  <div className="error-message">{inputErrors.age}</div>
                )}
              </div>
              <div>
                <label className="calculator-labeltext">
                  Monthly Savings *
                </label>
                <input
                  type="number"
                  className="calculator-input"
                  value={monthlySavings}
                  onChange={(event) =>
                    handleInputChange(event, "monthlySavings")
                  }
                />
                {inputErrors.monthlySavings && (
                  <div className="error-message">
                    {inputErrors.monthlySavings}
                  </div>
                )}
              </div>
            </div>

            <div className="calculatorbuttons">
              <button
                className="calculator-calculatebtn"
                onClick={handleCalculate}
              >
                Calculate
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

        {showResults && (
          <InvestmentPlannerResults
            monthlySalary={childmonthlySalary}
            age={childage}
            monthlySavings={childmonthlySavings}
          />
        )}
<div className="calculator-heading">
<h2 className="center-text">About Investment Planner</h2>

</div>
<div className="calculator-description">
  <div className="content">
    <div className="section">
      <b>1) How does the Investment Planner help?</b>
      <p>
        Whether you're a beginner who’s started with investing, or you're
        already a veteran investor, our Investment Planner can help you figure
        out how to meet your financial goals with diversified asset allocations.
        It helps to give a clear picture of how much you should ideally save &
        how much you should invest (financial planning), and where should you
        invest (asset allocation). We'll walk you through the basics of
        investing, tell you about different risks and considerations involved
        to put your money to work.
      </p>
    </div>
    <div className="section">
      <b>2) Why you should invest?</b>
      <p>
        Investing lets your money (that you’re not spending) be kept at a place
        where it can work for you. In simple terms, setting aside some money out
        of income to get money from it in the future is termed as an investment.
        The money you invest in stock and bonds can help governments or
        companies grow, and in the meantime, it will earn you a good amount of
        return.
      </p>
    </div>
    <div className="section">
      <b>3) How to decide your asset allocation?</b>
      <p>
        Variables Involved
        <br />
        Risk v/s Returns on Investments:
        <br />
        In general, investing is a trade-off between risk and return.
        Investments with higher return potential also have a higher risk
        potential. The out-of-danger investments sometimes barely beat the
        inflation rate. Finding the rightly balanced asset allocation for you
        will depend on your risk tolerance and age.
        <br />
        When it comes to investment planning, the closer you are to retirement,
        the more vulnerable it is to your investment portfolio. So, what's an
        investor supposed to do? Conventional wisdom says older investors who
        are getting closer to retirement should reduce their exposure to risk by
        shifting some of their investments from stocks to bonds.
      </p>
    </div>

  </div>
</div>

      </div>
    </div>
  );
}

export default InvestmentPlanner;
