import React, { useState, useEffect, useMemo } from "react";
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import './calculatoresults.css'

function LumpsumResults({ investmentAmount, expectedRateOfReturn, tenure }) {
  const [CorpusValue, setCorpusValue] = useState(0);
  const [TotalEarnings, setTotalEarnings] = useState(0);

  const data = useMemo(() => {
    const newData = [];
    for (let t = 0; t <= tenure; t++) {
      const corpus = investmentAmount * Math.pow(1 + (expectedRateOfReturn / 100), t);
      newData.push({ name: t.toString(), value: Math.ceil(corpus * 100) / 100 });
    }
    setCorpusValue(newData[newData.length - 1].value);
    setTotalEarnings(newData[newData.length - 1].value - investmentAmount);
    return newData;
  }, [investmentAmount, expectedRateOfReturn, tenure]);
  // Calculating the maximum value for the Y-axis on a chart,rounding up to the nearest hundred.
  const maxYAxisValue = Math.ceil(CorpusValue / 100) * 100;
  const numYAxisIntervals = 10;
  // Calculate the interval between Y-axis ticks based on the maximum value.
  const maxYAxisInterval = maxYAxisValue / numYAxisIntervals;

  const maxXAxisValue = tenure;

  // Generate Y-axis tick values evenly spaced between 0 and the maximum value.
  const yTicks = [];
  for (let i = 0; i <= numYAxisIntervals; i++) {
    yTicks.push(i * maxYAxisInterval);
  }

  return (
    <div className="calculator-results">
      <div className="calculator-results-flex-1">
        <p> Your Corpus Value : {Math.ceil(CorpusValue * 100) / 100}</p>
        <p> Total Earnings : {Math.ceil(TotalEarnings*100)/100} </p>
        <p> Total Deposited Amount : {Math.ceil(investmentAmount*100)/100}</p></div>
      <div className="calculator-results-flex-2">
      <div className="lumpsum-graph" >

        <ResponsiveContainer width="100%" height={300}>
          <LineChart
            data={data}
            margin={{
              top: 5,
              right: 30,
              left: 20,
              bottom: 5,
            }}
          >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="name" interval={Math.ceil(maxXAxisValue / 10)} />
            <YAxis domain={[0, maxYAxisValue]} ticks={yTicks} />
            <Tooltip />
            <Legend />
            <Line type="monotone" dataKey="value" stroke="#8884d8" activeDot={{ r: 8 }}   name={"Corpus Value"}
/>
          </LineChart>
        </ResponsiveContainer></div>
      </div>
    </div>
  )
}

export default LumpsumResults;
