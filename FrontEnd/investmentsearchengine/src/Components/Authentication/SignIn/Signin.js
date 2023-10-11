import React, { useState } from "react";
import "../AuthenticationPagesDesign/AuthenticationDesign.css";
import { useNavigate } from "react-router-dom";
import { FaEye, FaEyeSlash } from "react-icons/fa";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function SignIn() {
  const navigate = useNavigate();
  const [passwordVisible, setPasswordVisible] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [emailError, setEmailError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [user, setUser] = useState({
    emailId: "",
    password: "",
  });

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  };

  const validateEmail = () => {
    setEmailError(
      !email
        ? "Email ID is required"
        : !/\S+@\S+\.\S+/.test(email)
        ? "Please enter a valid email ID"
        : ""
    );
  };

  const validatePassword = () => {
    setPasswordError(!password ? "Password is required" : "");
  };

  const isFormValid = () => {
    return email && password && !emailError && !passwordError;
  };

  const handleSignIn = () => {
    validateEmail();
    validatePassword();
  
    if (isFormValid()) {
      console.log("Email:", user.emailId);
      console.log("password:", user.password);
  
      fetch(process.env.REACT_APP_lOGINWATCHLIST_API + "User/Login", {
        method: "POST",
        headers: {
          Accept: "text/plain",
          "Content-Type": "application/json",

          
        },
        body: JSON.stringify(user),
      })
        .then(async (data) => {
          if (data.status === 200) {
            var myData = await data.json();
            localStorage.setItem("token", myData.token.toString());
            localStorage.setItem("emailId", myData.emailId.toString());
            localStorage.setItem("userId", myData.userId);
            toast.success("Successfully logged in");
            navigate("/dashboard");
          } else if (data.status === 400) {
            var errorData = await data.text(); 
            toast.error(errorData); 
          }
        })
        .catch((err) => {
          console.error(err);
          toast.error("An error occurred while logging in.");
        });
    }
  };
  

  const navigateToSignUp = () => {
    navigate("/Authentication/signup");
  };

  return (
    <div className="SignIn">
      <div className="SignInCredentials">
        <div className="SignInContext">
          <span className="SignInLable">Sign In</span>
          <span className="SignInLableSubText">
            Welcome back! Please enter email id and password
          </span>
        </div>
        <div className="SignInContext">
          <label className="SigInInputLable">Email ID</label>
          <input
            type="text"
            className="SigInInputField"
            placeholder="example@kanini.com"
            value={email}
            onChange={(e) => {
              setEmail(e.target.value);
              setEmailError("");
              setUser({ ...user, emailId: e.target.value });
            }}
            onBlur={validateEmail}
          />
          {emailError && <span className="error-red-text">{emailError}</span>}
        </div>
        <div className="SignInContext">
          <label className="SigInInputLable">Password</label>
          <div className="password-input">
            <input
              type={passwordVisible ? "text" : "password"}
              placeholder="Password"
              className="SigInInputField"
              value={password}
              onChange={(e) => {
                setPassword(e.target.value);
                setPasswordError("");
                setUser({ ...user, password: e.target.value });
              }}
              onBlur={validatePassword}
            />
            <span
              className="password-toggle"
              onClick={togglePasswordVisibility}
            >
              {passwordVisible ? (
                <FaEyeSlash className="PasswordVisible" />
              ) : (
                <FaEye className="PasswordVisible" />
              )}
            </span>
          </div>
          {passwordError && (
            <span className="error-red-text">{passwordError}</span>
          )}
        </div>
      </div>
      <div className="SignInButtonContainer">
        <button
          className={`SignInButton ${!isFormValid() ? "disabled" : ""}`}
          onClick={handleSignIn}
          disabled={!isFormValid()}
        >
          SIGN IN
        </button>
        <div className="SignUpLink">
          <span>
            Don't have an account?{" "}
            <span id="SignUpClickHere" onClick={navigateToSignUp}>Sign Up</span>
          </span>
        </div>
      </div>
      <ToastContainer />
    </div>
  );
}

export default SignIn;