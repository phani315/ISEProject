import React, { useEffect, useState } from "react";
import "./Card.css";
import WatchList from "./WatchList";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function Card() {
  const [searchInput, setSearchInput] = useState("");
  const [companyData, setCompanyData] = useState([]);
  const [searchResults, setSearchResults] = useState([]);
  const [watchListUpdated, setWatchListUpdated] = useState(false);
  const [showDropdown, setShowDropdown] = useState(false);
  const [myUserWishlist, setMyUserWishlist] = useState([]);
  var token = localStorage.getItem("token");

  useEffect(() => {
    var token = localStorage.getItem("token");
    fetchCompanyData();
  }, [watchListUpdated]);

  const fetchCompanyData = async () => {
    try {
      const response = await fetch("http://localhost:5169/api/CompanyDetails");
      const data = await response.json();
      setCompanyData(data);
    } catch (error) {
      console.error("Error fetching company data:", error);
    }
  };

  // Handling search input
  const handleSearchInputChange = (e) => {
    const input = e.target.value;
    setSearchInput(input);
    // Filtering the company data based on search input
    const filteredCompanies = companyData.filter((company) =>
      company.companyName.toLowerCase().includes(input.toLowerCase())
    );
    setSearchResults(filteredCompanies);
    setShowDropdown(true);
  };

  const handleCompanyClick = (companyId) => {
    setSearchInput(companyId);
    setShowDropdown(false);
    setSearchInput("");
  };

  // Adding company
  const handleCompanyAdd = async (companyId) => {
    try {
      console.log("Adding company with ID:", companyId);
      console.log(localStorage.getItem("userid"));
      const response = await fetch(
        process.env.REACT_APP_lOGINWATCHLIST_API+"WatchList/AddingToWatchList",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            accept: "text/plain",
            Authorization: "Bearer " + token,
          },
          body: JSON.stringify({
            userId: localStorage.getItem("userId"),
            companyId: companyId,
          }),
        }
      );
      if (response.status === 200) {
        console.log("Company added to wishlist successfully");
        setWatchListUpdated(new Date().getTime());
        setSearchInput("");
        toast.success("Company successfully added to watchlist");
      }
      if (response.status === 400) {
        const data=await response.json();
        toast.warning(data.errorMessage);
      } 
    } catch (error) {
      console.error("Error adding company to wishlist:", error);
    }
    setShowDropdown(false);
    setSearchInput("");
  };

  const updateMyUser = (myUserWishlist) => {
    // Updating the myUserWishlist data in the Card component
    console.log("Updated myUser:", myUserWishlist);
    setMyUserWishlist(myUserWishlist);
  };

  return (
    <div className="cardContainer">
      <div className="cardHeadContainer">
        <div className="subContainer1">
          <i
            className="fas fa-eye"
            id="eyeIcon"
          ></i>
          <div className="watchlist-text">
            Watchlist companies &#40;{myUserWishlist.length}&#41;
          </div>
       
        </div>
        <div className="search-text-and-box">
          <div className="searchAdd">
            <span>Search</span>
            <span>&nbsp;&amp;&nbsp;</span>
            <span>Add</span>
          </div>
          <div className="searchbox">
            <div className="searchInput">
              <button type="submit" className="searchButton">
                <i
                id="searchIcon"
                  className="fas fa-search"
                ></i>
              </button>
              <input
                type="text"
                placeholder="Search a company"
                className="searchInputField"
                value={searchInput}
                onChange={handleSearchInputChange}
              />
              {searchInput && showDropdown && (
                <div className="searchDropdown">
                  {searchResults.length === 0 ? (
                    <div className="noCompanyFound">No company found</div>
                  ) : (
                    searchResults.map((company) => (
                      <div className="searchResultItem" key={company.companyId}>
                        <div
                          className="companyName"
                          onClick={() => handleCompanyClick(company.companyId)}
                        >
                          {company.companyName}
                        </div>
                        <button
                          className="addButton"
                          onClick={() => handleCompanyAdd(company.companyId)}
                        >
                          <i className="fas fa-plus"></i>
                        </button>
                      </div>
                    ))
                  )}
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
      <div className="watchlistContainer">
        <WatchList
          watchListUpdated={watchListUpdated}
          updateMyUser={updateMyUser}
        />
      </div>
    </div>
  );
}

export default Card;
