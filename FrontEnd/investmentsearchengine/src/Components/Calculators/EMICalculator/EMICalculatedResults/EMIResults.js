import React, { useState, useEffect } from "react";
import { PieChart, Pie, Tooltip,ResponsiveContainer,Cell } from "recharts";
import './EmiResults.css'
function EMIResults({ loanamount, loantenure, interestrate }) {
  const [emi, setEmi] = useState('');
  const [interestAccrued, setInterestAccrued] = useState(0);
  const [data02, setData02] = useState([
    { name: "Loan Amount", value: 0 },
    { name: "Interest Accrued", value: 0 },
  ]);

  useEffect(() => {
    calculateEmi();
  }, [loanamount, loantenure, interestrate]);

  const calculateEmi = () => {
    const principal = parseFloat(loanamount);
    const annualInterestRate = parseFloat(interestrate);
    const monthlyInterestRate = annualInterestRate / 12 / 100; // Monthly interest rate
    const loanTenureYears = parseInt(loantenure);

    if (isNaN(principal) || isNaN(annualInterestRate) || isNaN(loanTenureYears)) {
      setEmi('Invalid input');
      return;
    }

    if (principal <= 0 || annualInterestRate <= 0 || loanTenureYears <= 0) {
      setEmi('Invalid input. Please enter positive values.');
      return;
    }

    const loanTenureMonths = loanTenureYears * 12; // Convert loan tenure from years to months
    const r = Math.pow(1 + monthlyInterestRate, loanTenureMonths);
    const emiValue = (principal * monthlyInterestRate * r) / (r - 1);
    setEmi(emiValue.toFixed(2));

    // Calculate Interest Accrued
    const totalPayment = emiValue * loanTenureMonths;
    const interestAccruedValue = totalPayment - principal;
    setInterestAccrued(interestAccruedValue);

    // Update data02 array with loan amount and interest
    setData02([
      { name: "Loan Amount", value: principal },
      { name: "Interest Accrued", value: interestAccruedValue },
    ]);
  };
  const COLORS = ["#8884d8", "#82ca9d"];

  return (
    <div className="emiresults">
      <div className="emivalues">
        <p>Total Loan Amount: {loanamount}</p>
        <p>EMI Value: {emi}</p>
        <p>Interest Accured: {interestAccrued}</p>
      </div>
      <div  className="emipiechart">
        <ResponsiveContainer>
          <PieChart>
            <Pie
              dataKey="value"
              isAnimationActive={true}
              data={data02}
              cx="50%"
              cy="50%"
              outerRadius={90}
              label
            >
              {/* Map custom colors to data segments */}
              {data02.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
              ))}
            </Pie>
            <Tooltip />
          </PieChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
}

export default EMIResults;
