import React, { useState } from "react";
import "../Navbar/Navbar.css";
import KaniniLogo from "../../Assets/Images/Navbar/KaniniLogo.svg";
import { Link, useNavigate } from "react-router-dom";
import ProfileLogo from "../../Assets/Images/Navbar/Profile.png";
import { FaRegEye } from "react-icons/fa";
import { RiLogoutBoxRLine } from "react-icons/ri";

function Navbar() {
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();
  return (
    <div className="nav">
      <div className="logo">
        <img src={KaniniLogo} alt="KaniniLogo" className="kaniniLogo" />
      </div>
      <div className="profile">
        <ul>
          <li>
            <img
              src={ProfileLogo}
              className="profileLogo"
              onClick={() => {
                setOpen(!open);
              }}
            />
            <div className="profileItems">
              <ul
                className={open ? "profileItemsDisplay" : "profileItemsHidden"}
              >
                <li>
                  <Link
                    to="/dashboard/watchlist"
                    className="profileItemInfo"
                    style={{ color: "#000000" }}
                  >
                    <div className="NavbarProfilIconDivison">
                      <div>
                        <FaRegEye className="NavbarProfileIcon" />
                      </div>
                      <div className="profileInfo">WatchList</div>
                    </div>
                  </Link>
                </li>
                <li>
                  <Link
                    onClick={() => {
                      localStorage.clear();
                    }}
                    to="/"
                    className="profileItemInfo"
                    style={{ color: "#000000" }}
                  >
                    <div className="NavbarProfilIconDivison">
                      <div>
                        <RiLogoutBoxRLine className="NavbarProfileIcon" />
                      </div>
                      <div className="profileInfo">Logout</div>
                    </div>
                  </Link>
                </li>
              </ul>
            </div>
          </li>
        </ul>
      </div>
    </div>
  );
}
export default Navbar;
