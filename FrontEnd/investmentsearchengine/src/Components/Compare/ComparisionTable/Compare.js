import React, { useEffect, useState } from "react";
import "./Compare.css";
import addtocompare from "../../../Assets/Images/ComparePage/add_318-749750.avif";
import Select from "react-select";
import compareicon from "../../../Assets/Images/ComparePage/compareicon.png";
import { useParams } from "react-router-dom";
import GraphComponent from "../ComparisionGraph/ComparisionGraph";
import ComparePageheader from "../ComparePageHeader/ComparePageHeader";
function Compare() {
  var token = localStorage.getItem("token");
  // State for storing essential data of the first company
  const [essentialsData, setessentialsData] = useState({
    marketCap: 0,
    enterpriceValue: 0,
    noOfShares: 0,
    divYield: 0,
    cash: 0,
    promoterHolding: 0,
    price: 0,
    bookValue: 0,
    priceToBook: 0,
    priceToEarning: 0,
    eps: 0,
    netIncome: 0,
    sector: "",
  });
  // State for storing essential data of the second company
  const [secondCompanyEssentialsData, setSecondCompanyEssentialsData] =
    useState({
      marketCap: 0,
      enterpriceValue: 0,
      noOfShares: 0,
      divYield: 0,
      cash: 0,
      promoterHolding: 0,
      price: 0,
      bookValue: 0,
      priceToBook: 0,
      priceToEarning: 0,
      eps: 0,
      netIncome: 0,
      sector: "",
    });
  // State for storing details of the currently selected company
  const [selectedCompanyDetails, setSelectedCompanyDetails] = useState([]);
  const [companyDetails, setCompanyDetails] = useState([]);
  const [companyNames, setCompanyNames] = useState([]);
  const [searchResults, setSearchResults] = useState([]);
  const { id } = useParams();
  const [isButtonClicked, setIsButtonClicked] = useState(false);
  const [selectedCompanyId, setSelectedCompanyId] = useState(null);
  const [selectedOption, setSelectedOption] = useState(null);
  const [isClicked, setIsClicked] = useState(false);
  const [companyName2, setcompanyName2] = useState(null);

  const fetchCompanyEssentials = async (companyId, isInitial = true) => {
    try {
      const response = await fetch(
        process.env.REACT_APP_COMPANYESSENTIALS_API +
          "CompanyEssentials/GetEssentialsByCompany?id=" +
          companyId,
        {
          method: "POST",
          headers: {
            accept: "text/plain",
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,
          },
        }
      );
      if (response.ok) {
        const essentialsData = await response.json();
        if (isInitial) {
          setessentialsData(essentialsData);
          isInitial = false;
        } else {
          setSecondCompanyEssentialsData(essentialsData);
        }
      } else {
        console.error("Error:", response.status);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  const getCompanyDetails = async (companyId) => {
    try {
      const response = await fetch(
        process.env.REACT_APP_COMPANYDETAILS_API + "CompanyDetails/GetCompanyDetailsById/" + id,
        {
          method: "POST",
          headers: {
            accept: "text/plain",
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,

          },
        }
      );
      if (response.ok) {
        const data = await response.json();
        setCompanyDetails(data);
      } else {
        console.error("Error:", response.status);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };
  const getCompanyNames = async () => {
    try {
      const response = await fetch(
        process.env.REACT_APP_COMPANYDETAILS_API + `CompanyDetails/GetAllCompanyDetails`, {
          method: "GET",
          headers: {
            accept: "text/plain",
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,

          },
        }
      );
      if (response.ok) {
        const data = await response.json();
        setSelectedCompanyDetails(data);
        // Filtering out the company with the same ID as the one in the URL parameter
        const filteredData = data.filter(
          (company) => company.companyId !== parseInt(id, 10)
        );

        const names = filteredData.map((company) => company.companyName);
        setCompanyNames(names);
        setSearchResults(names);
      } else {
        console.error("Error:", response.status);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  useEffect(() => {
    var token = localStorage.getItem("token");
    const fetchData = async () => {
      try {
        await Promise.all([
          fetchCompanyEssentials(id),
          getCompanyDetails(),
          getCompanyNames(),
        ]);
      } catch (error) {
        console.error("Error:", error);
      }
    };

    fetchData();
  }, []);

  const handleInputChange = (inputValue) => {
    const results = companyNames.filter((company) =>
      company.toLowerCase().includes(inputValue.toLowerCase())
    );

    setSearchResults(results);
  };

  const handleSearchItemClick = async (name) => {
    setcompanyName2(name);

    const selectedCompany = selectedCompanyDetails.find(
      (company) => company.companyName.toLowerCase() === name.toLowerCase()
    );

    if (selectedCompany) {
      const selectedCompanyId = selectedCompany.companyId;
      setIsClicked(true);
      setSelectedOption("");
      setIsButtonClicked(true); // Setting the state to true when the button is clicked

      // Filtering out the selected company name from the search results
      setSearchResults((prevResults) =>
        prevResults.filter((result) => {
          const company = selectedCompanyDetails.find(
            (company) =>
              company.companyName.toLowerCase() === result.toLowerCase()
          );
          return company && company.companyId !== selectedCompanyId;
        })
      );
      await fetchCompanyEssentials(selectedCompanyId, false);
      setSelectedCompanyId(selectedCompanyId);
    }
  };

  const handleSelect = (option) => {
    setSelectedOption(option);
  };
  function toTitleCase(str) {
    return str.replace(/([A-Z])/g, " $1").replace(/^./, function (str) {
      return str.toUpperCase();
    });
  }

  return (
    <div className="comparepage">
      <div className="comparepagebackbutton">
        <ComparePageheader id={id}></ComparePageheader>
      </div>
      <div className="comparepagebody">
        <div className="ComparePage">
          <div className="comparepage-flex-container">
            <div className="comparepage-flex-1">
              <div>
                <label type="text" className="comparepage-companyname">
                  {companyDetails.companyName}
                </label>
              </div>
              <div>
                <label className="sectorlabel">SECTOR :</label>
                <label type="text" className="sectortext">
                  {" "}
                  {companyDetails.sector}
                </label>
              </div>
            </div>
            <div className="comparepage-flex-2">
              <div className="searchcard">
                <div className="searchbox">
                  <div className="comparepage-text">
                    {" "}
                    <p>Select a peer to compare</p>
                  </div>

                  <div className="searchandadd">
                    <Select
                      className="search"
                      placeholder="Select a peer to compare"
                      options={searchResults
                        .filter(
                          (result) =>
                            result !== id &&
                            result.toLowerCase() !== companyName2?.toLowerCase()
                        )
                        .map((result) => ({ value: result, label: result }))}
                      value={selectedOption}
                      onChange={handleSelect}
                      onInputChange={handleInputChange}
                      isSearchable={true}
                      autosize={true}
                    />

                    {selectedOption && selectedOption.value && (
                      <img
                        alt="add to compare icon"
                        className="addtocompareicon"
                        src={addtocompare}
                        onClick={() =>
                          handleSearchItemClick(selectedOption.value)
                        }
                      />
                    )}
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="addpeertext">
            <p>
              Add a peer from above to compare this stock.You can compare upto
              two peers{" "}
            </p>
          </div>

          <div className="compare-container">
            <div className="compareiconwithtext">
              <img
                alt="compare icon"
                className="compareicon"
                src={compareicon}
              ></img>
              <div className="companycomparisiontext">
                <b> Company comparison for {companyDetails.companyName}</b>
              </div>
            </div>
            <div className="compare-table">
              <table>
                <thead>
                  <tr>
                    <th>Comparison Parameters</th>
                    <th>{companyDetails.companyName}</th>
                    {isClicked || selectedOption ? (
                      <th>{companyName2}</th>
                    ) : (
                      <th></th>
                    )}
                  </tr>
                </thead>
                <tbody>
                  {Object.keys(essentialsData).map(
                    (property) =>
                      !["essenID", "companyID"].includes(property) && (
                        <tr key={property}>
                          <td>{toTitleCase(property)}</td>
                          <td>{essentialsData[property]}</td>
                          <td>
                            {secondCompanyEssentialsData[property] === 0
                              ? ""
                              : secondCompanyEssentialsData[property]}
                          </td>
                        </tr>
                      )
                  )}
                </tbody>
              </table>
            </div>

            <div></div>
            {isButtonClicked ? (
              <GraphComponent
                companyId1={id}
                companyId2={selectedCompanyId}
                companyName1={companyDetails.companyName}
                companyName2={companyName2}
              />
            ) : null}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Compare;
