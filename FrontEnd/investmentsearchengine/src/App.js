import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import UserProtected from "./Components/Protected/UserProtected";
import HomeNavbar from "./Components/HomePage/HomeNavbar/HomeNavbar";
import Authentication from "./Components/Authentication/AuthenticationPagesDesign/AuthenticationDesign";
import Dashboard from "./Components/Dashboard/Dashboard";

function App() {
  var token;
  return (
    <div>
      <ToastContainer />
      <BrowserRouter>
        <Routes>
          <Route path="/*" element={<HomeNavbar />} />
          <Route path="/Authentication/*" element={<Authentication />} />
          <Route
            path="/dashboard/*"
            element={
              <UserProtected token={token}>
                <Dashboard />{" "}
              </UserProtected>
            }
          />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
