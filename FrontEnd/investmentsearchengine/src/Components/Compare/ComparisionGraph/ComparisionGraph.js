import React, { useEffect, useState } from "react";
import '../ComparisionGraph/ComparisionGraph.css'
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
} from "recharts";

function GraphComponent({ companyId1, companyId2, companyName1, companyName2 }) {
  var token = localStorage.getItem("token");
  const [isSecondCompanyDataAvailable, setIsSecondCompanyDataAvailable] =
    useState(false);
  const [isFirstCompanyDataAvailable, setIsFirstCompanyDataAvailable] =
    useState(false);

  const [transformedData, setTransformedData] = useState([]);

  const mergeDataArrays = (array1, array2) => {
    const mergedData = array1
      .map((dataPoint) => {
        const month1 = new Date(dataPoint.date).toLocaleString("default", {
          month: "short",
        });
        const matchingDataPoint = array2.find((item) => {
          const month2 = new Date(item.date).toLocaleString("default", {
            month: "short",
          });
          return month1 === month2;
        });

        if (matchingDataPoint) {
          return {
            date: month1,
            stockValue1: dataPoint.stockValue,
            stockValue2: matchingDataPoint.stockValue,
          };
        }
        return null;
      })
      .filter((item) => item !== null);

    return mergedData;
  };

  useEffect(() => {
    const fetchDataAndTransform = async () => {
      try {
        if (!companyId1 || !companyId2) {
          return; // Make sure both company IDs are available
        }

        // Fetch stock data for companyId1
        const response1 = await fetch(
          process.env.REACT_APP_STOCKPRICE_API +
            `StockPrice/GetAllStockDetailsCompanyID?companyId=${companyId1}`,
          {
            method: "POST",
            headers: {
              accept: "text/plain",
              "Content-Type": "application/json",
              Authorization: "Bearer " + token,
            },
          }
        );
        if (response1.ok) {
          setIsFirstCompanyDataAvailable(true);

          const data = await response1.json();
          const transformedData1 = data.stockTransactions.reduce(
            (acc, dataPoint) => {
              const date = new Date(dataPoint.date);
              const month = date.toLocaleString("default", { month: "short" });

              if (!acc[month] || dataPoint.stockValue > acc[month].stockValue) {
                acc[month] = dataPoint;
              }

              return acc;
            },
            {}
          );
          const transformedDataArray1 = Object.values(transformedData1);

          // Fetch stock data for companyId2
          const response2 = await fetch(
            process.env.REACT_APP_STOCKPRICE_API +
              `StockPrice/GetAllStockDetailsCompanyID?companyId=${companyId2}`,
            {
              method: "POST",
              headers: {
                accept: "text/plain",
                "Content-Type": "application/json",
                Authorization: "Bearer " + token,
              },
            }
          );
          if (response2.ok) {
            setIsSecondCompanyDataAvailable(true);

            const data = await response2.json();
            const transformedData2 = data.stockTransactions.reduce(
              (acc, dataPoint) => {
                const date = new Date(dataPoint.date);
                const month = date.toLocaleString("default", {
                  month: "short",
                });

                if (
                  !acc[month] ||
                  dataPoint.stockValue > acc[month].stockValue
                ) {
                  acc[month] = dataPoint;
                }

                return acc;
              },
              {}
            );
            const transformedDataArray2 = Object.values(transformedData2);

            // Merge the transformed data arrays
            const mergedData = mergeDataArrays(
              transformedDataArray1,
              transformedDataArray2
            );
            setTransformedData(mergedData);
          } else {
            setIsSecondCompanyDataAvailable(false);

            console.error("Error:", response2.status);
          }
        } else {
          setIsSecondCompanyDataAvailable(false);

          console.error("Error:", response1.status);
        }
      } catch (error) {
        console.error("Error:", error);
      }
    };

    fetchDataAndTransform();
  }, [companyId1, companyId2]);

  return (
    <div className="line-chart-scroll-container">
      {isFirstCompanyDataAvailable && isSecondCompanyDataAvailable ? (
        transformedData.length > 0 ? (
          <div className="line-chart-container">
            <div className="scrolling-container">
              <LineChart
                width={1365} 
                height={400}
                data={transformedData}
                margin={{
                  top: 5,
                  right: 30,
                  left: 50,
                  bottom: 5,
                }}
              >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="date" padding={{ left: 30, right: 30 }} />
                <YAxis />
                <Tooltip />
                <Legend />
                <Line
                  type="monotone"
                  dataKey="stockValue1"
                  stroke="#8884d8"
                  activeDot={{ r: 8 }}
                  name={companyName1}
                />
                <Line
                  type="monotone"
                  dataKey="stockValue2"
                  stroke="#82ca9d"
                  activeDot={{ r: 8 }}
                  name={companyName2}
                />
              </LineChart>
              
            </div>
          </div>
        ) : (
          <p className="custom-message">
            Stocks Data is not available to compare using the graph.
          </p>
        )
      ) : (
        <center>
          <p className="custom-message">
            Stock data for one or both companies is not available to compare
            with the graph.
          </p>
        </center>
      )}
    </div>
  );
}

export default GraphComponent;
