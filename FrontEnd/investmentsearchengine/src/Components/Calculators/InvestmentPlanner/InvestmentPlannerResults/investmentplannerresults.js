import React, { useState, useEffect } from "react";
import { PieChart, Pie, Tooltip, ResponsiveContainer, Cell } from "recharts";
import './investmentresults.css'
import InvestmentPlannerResultsImage from "../../../../Assets/Images/Calculators/InvestmentPlannerResults.png";

function InvestmentPlannerResults({ monthlySalary, age, monthlySavings }) {
  const [idealMonthlyInvestment, setIdealMonthlyInvestment] = useState("");
  const [deficit, setDeficit] = useState("");
  const [allocationEquity, setAllocationEquity] = useState("");
  const [allocationDebt, setAllocationDebt] = useState("");

  useEffect(() => {
    calculateInvestmentPlan();
  }, [monthlySalary, age, monthlySavings]);

  const calculateInvestmentPlan = () => {
    const salary = parseFloat(monthlySalary);
    const savings = parseFloat(monthlySavings); 

    if (isNaN(salary) || isNaN(savings)) {
      setIdealMonthlyInvestment("Invalid input");
      return;
    }

    const idealInvestmentPercentage = 0.2; // 20% of monthly salary
    const idealMonthlyInvestment = (salary * idealInvestmentPercentage).toFixed(2);
    setIdealMonthlyInvestment(idealMonthlyInvestment);

    const deficit = (savings - idealMonthlyInvestment).toFixed(2);
    setDeficit(deficit);

    const allocationEquityPercentage = 0.7; // 70% in equity
    const allocationDebtPercentage = 0.3; // 30% in debt
    const allocationEquity = (idealMonthlyInvestment * allocationEquityPercentage).toFixed(2);
    const allocationDebt = (idealMonthlyInvestment * allocationDebtPercentage).toFixed(2);
    setAllocationEquity(allocationEquity);
    setAllocationDebt(allocationDebt);
  };
  const COLORS = ["#8884d8", "#82ca9d"];


  return (
    <div className="investment-results">
      <div className="resultsimage">
        <img src={InvestmentPlannerResultsImage}></img>
        </div>
        <div className="investment-planner-results">
      <div className="investment-values">
        <p>Ideal Monthly Investment: {idealMonthlyInvestment}</p>
        <p>Deficit: {deficit}</p>
        <p>Allocation in Equity: {allocationEquity}</p>
        <p>Allocation in Debt: {allocationDebt}</p>
      </div>
      <div className="investment-piechart" >
      <ResponsiveContainer width="100%" height={300}> 

          <PieChart>
            <Pie
              dataKey="value"
              isAnimationActive={true}
              data={[
                { name: "Equity", value: parseFloat(allocationEquity) },
                { name: "Debt", value: parseFloat(allocationDebt) },
              ]}
              cx="50%"
              cy="50%"
              outerRadius={90}
              label
            >
              <Cell key={`cell-equity`} fill={COLORS[0]} />
              <Cell key={`cell-debt`} fill={COLORS[1]} />
            </Pie>
            <Tooltip />
          </PieChart>
        </ResponsiveContainer>
      </div></div>
    </div>
  );
}

export default InvestmentPlannerResults;
