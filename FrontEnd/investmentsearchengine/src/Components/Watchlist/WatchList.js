import React, { useEffect, useState } from "react";
import "./WatchList.css";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Pagination from "./Pagination";

function WatchList({ watchListUpdated, updateMyUser }) {
  const [userId, setUserId] = useState(localStorage.getItem("userId"));
  const [myUserWishlist, setMyUserWishlist] = useState([]);
  const [companyData, setCompanyData] = useState([]);
  const [MCapdata, setMCapdata] = useState([]);
  const [stockdetails, setStockDetails] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [rowsPerPage] = useState(5);
  const [deletionOccurred, setDeletionOccurred] = useState(false);
  var token = localStorage.getItem("token");
  const totalRows = myUserWishlist.length;
  const totalPages = Math.ceil(totalRows / rowsPerPage);
  const startIndex = (currentPage - 1) * rowsPerPage;
  const endIndex = startIndex + rowsPerPage;
  const currentRows = myUserWishlist.slice(startIndex, endIndex);

  useEffect(() => {
    const fetchWishlistData = async () => {
      try {
        var token = localStorage.getItem("token");
        const response = await fetch(
          process.env.REACT_APP_lOGINWATCHLIST_API+"WatchList/GetAllWatchList",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
              Authorization: "Bearer " + token,
            },
            body: JSON.stringify({ UserId: userId }),
          }
        );

        if (response.status === 200) {
          const myUserWishlist = await response.json();
          setMyUserWishlist(myUserWishlist);
          updateMyUser(myUserWishlist);
        }
      } catch (error) {
        console.log(error);
      }
    };

    fetchWishlistData();
  }, [watchListUpdated, deletionOccurred]);

  useEffect(() => {
    if (myUserWishlist.length > 0) {
      fetchCompanyData();
      fetchMCapData();
      fetchStockDetails();
    }
  }, [myUserWishlist, deletionOccurred]);

  const fetchCompanyData = async () => {
    const promises = myUserWishlist.map(async (user) => {
      try {
        const response = await fetch(
          process.env.REACT_APP_COMPANYDETAILS_API+`CompanyDetails/${user.companyIds}`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
              Authorization: "Bearer " + token,
            },
          }
        );

        if (response.status === 200) {
          const companyData = await response.json();
          console.log(companyData);
          return companyData;
        }
        return null;
      } catch (error) {
        console.log(error);
        return null;
      }
    });

    const companiesData = await Promise.all(promises);
    setCompanyData(companiesData);
  };

  const fetchMCapData = async () => {
    const promises = myUserWishlist.map(async (user) => {
      try {
        const response = await fetch(
          process.env.REACT_APP_COMPANYESSENTIALS_API+`CompanyEssentials/GetEssentialsByCompany?id=${user.companyIds}`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
              Authorization: "Bearer " + token,
            },
            body: JSON.stringify({ user }),
          }
        );

        if (response.status === 200) {
          const MCapdata = await response.json();
          return MCapdata;
        }
        return null;
      } catch (error) {
        console.log(error);
        return null;
      }
    });

    const MCapData = await Promise.all(promises);
    setMCapdata(MCapData);
  };

  const fetchStockDetails = async () => {
    const promises = myUserWishlist.map(async (user) => {
      try {
        const response = await fetch(
          process.env.REACT_APP_STOCKPRICE_API+`StockPrice/GetStockDetailsAveragesCompanyID?companyId=${user.companyIds}`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
              Authorization: "Bearer " + token,
            },
          }
        );

        if (response.status === 200) {
          const stockdetails = await response.json();
          return stockdetails;
        }
        return null;
      } catch (error) {
        console.log(error);
        return null;
      }
    });

    const stockDetails = await Promise.all(promises);
    setStockDetails(stockDetails);
  };

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  const handleDelete = async (user) => {
    const { companyIds } = user;
    console.log("Deleting a company with ID:", companyIds);

    try {
      const response = await fetch(
        process.env.REACT_APP_lOGINWATCHLIST_API+"WatchList/RemovingFromWatchList",
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
            accept: "application/json",
            Authorization: "Bearer " + token,
          },
          body: JSON.stringify({ userId: userId, companyId: companyIds }),
        }
      );
      if (response.status === 200) {
        setDeletionOccurred(new Date().getTime());
        toast.success("Company removed from the watchlist");
      } else {
        console.log(response);
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="watchListContainer">
      {totalRows === 0 ? (
        <div>Your watchlist is empty.</div>
      ) : (
        <>
          <table className="watchListTable">
            <thead>
              <tr>
                <th>S.NO.</th>
                <th className="company">COMPANY</th>
                <th className="price">PRICE</th>
                <th>MCAP</th>
                <th>52 WK HIGH</th>
                <th>52 WK LOW</th>
                <th>DAY LOW</th>
                <th>DAY HIGH</th>
                <th>DELETE</th>
              </tr>
            </thead>
            <tbody>
              {currentRows.map((user, index) => {
                const companyIndex = startIndex + index;

                return (
                  <tr key={index}>
                    <td>{companyIndex + 1}</td>
                    <td className="company">
                      {companyData[companyIndex]?.companyName}
                    </td>
                    <td>{MCapdata[companyIndex]?.price}</td>
                    <td>{MCapdata[companyIndex]?.marketCap}</td>
                    <td>{stockdetails[companyIndex]?.yearHigh}</td>
                    <td>{stockdetails[companyIndex]?.yearLow}</td>
                    <td>{stockdetails[companyIndex]?.todayHigh}</td>
                    <td>{stockdetails[companyIndex]?.todayLow}</td>
                    <td>
                      <button
                        onClick={() => handleDelete(user)}
                        className="deleteButton"
                      >
                        <i className="fas fa-trash-alt"></i>
                      </button>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
          {totalPages > 1 && (
            <Pagination
              currentPage={currentPage}
              totalPages={totalPages}
              onPageChange={handlePageChange}
            />
          )}
        </>
      )}
    </div>
  );
}

export default WatchList;
