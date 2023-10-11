import React, { useEffect, useState } from "react";
import "./LandingPage.css";
import { Link } from "react-router-dom";
import bg2 from "../../Assets/Images/LandingPage/plus image.png";
import { useNavigate } from "react-router-dom";

function LandingPage() {
  const [searchInput, setSearchInput] = useState("");
  const [companyDetails, setCompanyDetails] = useState([]);
  const [trendingCompanies, setTrendingCompanies] = useState([]);
  const navigate = useNavigate();
  const [LandingApi, SetLandingApi] = useState(false);
  const [searchResults, setSearchResults] = useState([]); 


  useEffect(() => {
    fetchCompanyDetails();
    fetchData();

  }, []);
  var token = localStorage.getItem("token");
  useEffect(() => {
    if (searchInput.trim() !== "") { 
      fetchSearchedCompanies();
    } else {
      setSearchResults([]);  
    }
  }, [searchInput]);

  const fetchData = async () => {
    const headers = new Headers({
      accept: "application/json",
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    });
    const trendingRequest = new Request(
      process.env.REACT_APP_COMPANYESSENTIALS_API +
        "CompanyEssentials/GetFilteredCompanies/filtered-companies",
      {
        method: "GET",
        headers: headers,
      }
    );

    const detailsRequest = new Request(
      process.env.REACT_APP_COMPANYDETAILS_API + "CompanyDetails/GetAllCompanyDetails",
      {
        method: "GET",
        headers: headers,
      }
    );
    try {
      const trendingResponse = await fetch(trendingRequest);
      const detailsResponse = await fetch(detailsRequest);

      if (trendingResponse.ok && detailsResponse.ok) {
        const trendingData = await trendingResponse.json();
        console.log(trendingData);
        const detailsData = await detailsResponse.json();
        const trendingCompanyIds = trendingData.map((item) => item.companyID);
        const filteredDetailsData = detailsData.filter((item) =>
          trendingCompanyIds.includes(item.companyId)
        );
        setTrendingCompanies(filteredDetailsData);
      } else {
        console.log("Failed to fetch data");
        SetLandingApi(true);
      }
    } catch (error) {
      console.log(error);
      SetLandingApi(true);
    }
  };

  const fetchSearchedCompanies = async () => {
    try {
      const response = await fetch(
        process.env.REACT_APP_COMPANYDETAILS_API +
          `CompanyDetails/SearchCompanies?searchTerm=${searchInput}`,
        {
          method: "POST",
          headers: {
            accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,
          },
        }
      );

      if (response.ok) {
        const data = await response.json();
        setSearchResults(data); // Set search results when fetching data
      } else {
        console.log("Failed to fetch company details");
      }
    } catch (error) {
      console.log(error);
    }
  };

  const fetchCompanyDetails = async () => {
    try {
      const response = await fetch(
        process.env.REACT_APP_COMPANYDETAILS_API + "CompanyDetails/GetAllCompanyDetails",
        {
          method: "GET",
          headers: {
            accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,
          },
        }
      );

      if (response.ok) {
        const data = await response.json();
        setCompanyDetails(data);
      } else {
        console.log("Failed to fetch company details");
      }
    } catch (error) {
      console.log(error);
    }
  };

  const handleSearchChange = (event) => {
    setSearchInput(event.target.value);
  };

  const handleCompanyClick = (companyId) => {
    localStorage.setItem("companyId", companyId);
    navigate(`/dashboard/stockdetails/${companyId}`);
  };

  const filteredCompanies = searchInput
    ? companyDetails.filter(
        (company) =>
          company.companyName
            .toLowerCase()
            .startsWith(searchInput.toLowerCase()) ||
          (company.nse &&
            company.nse.toLowerCase().startsWith(searchInput.toLowerCase()))
      )
    : [];

  if (LandingApi) {
    return <div>No Services are available at this moment</div>;
  }

  return (
    <div className="LandingPageDisplay">
      <div className="LandingPageContainer">
        <div className="ScrollableContainer"></div>
        <div className="LandingPageContainer">
          <div className="LandingPagebackground">
            <div className="LandingPagemainText">Investing Search Engine</div>
            <div className="LandingPagesubText">
              The Modern Stock Screener that helps you pick better stocks
            </div>

            <div className="LandingPagewrapper">
              <button type="submit" className="LandingsearchButton">
                <i className="fas fa-search" style={{ color: "white" }}></i>
              </button>

              <input
                className="LandingPageinput"
                placeholder="Search for stocks"
                type="text"
                value={searchInput}
                onChange={handleSearchChange}
              />
              {searchResults.length > 0 && searchInput.length > 0 && (
                <div className="LandingPagesearchResults">
                  {searchResults.map((company, index) => (
                    <div
                      key={index}
                      className="searchresultsCompanyNameNSE"
                      style={{ cursor: "pointer" }}
                      onClick={() => handleCompanyClick(company.companyId)}
                    >
                      <p className="LandingPageCompanyName">
                        {company.companyName}
                      </p>
                      <p className="landingPageNSE">NSE : {company.nse}</p>
                    </div>
                  ))}
                </div>
              )}
              {searchResults.length === 0 && searchInput.length > 0 && (
                <div className="LandingPagesearchResults">
                  <div className="searchresultsCompanyNameNSE">
                    <p className="LandingPageCompanyName">
                      No companies available matching your search criteria.
                    </p>
                  </div>
                </div>
              )}
            </div>

            <div className="LandingPageTrendingText">What's Trending</div>
            <img src={bg2} alt="logo" height={20} />
            <div>
              <div className="LandingPagelogo">
                <div className="TrendingCompanies">
                  {trendingCompanies.map((trendingCompany) => {
                    const companyDetail = companyDetails.find(
                      (company) =>
                        company.companyId === trendingCompany.companyId
                    );
                    if (companyDetail && companyDetail.image) {
                      return (
                        <Link
                          to={`/dashboard/stockdetails/${trendingCompany.companyId}`}
                          key={trendingCompany.companyId}
                          onClick={() => {
                            console.log(
                              "Clicked on Trending Company with Company ID:",
                              trendingCompany.companyId
                            );
                          }}
                        >
                          <img
                            className="trendingimages"
                            src={companyDetail.image}
                            alt={trendingCompany.companyName}
                          />
                        </Link>
                      );
                    }
                  })}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default LandingPage;