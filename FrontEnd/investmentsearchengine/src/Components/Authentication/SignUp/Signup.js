import React, { useState } from "react";
import "../AuthenticationPagesDesign/AuthenticationDesign.css";
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function SignUp() {
  const navigate = useNavigate();
  const [passwordVisible, setPasswordVisible] = useState(false);
  const [confirmpasswordVisible, setConfirmPasswordVisible] = useState(false);
  const [name, setName] = useState("");
  const [dob, setDob] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [nameError, setNameError] = useState("");
  const [emailError, setEmailError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [confirmPasswordError, setConfirmPasswordError] = useState("");
  const [dobError, setDobError] = useState("");
  const [phoneNumberError, setPhoneNumberError] = useState("");
  const [user, setUser] = useState({
    emailId: "",
    userInfo: {
      name: "",
      phoneNumber: "",
      dateOfBirth: "",
    },
    passwordClear: "",
  });

  const isFormValid=()=>{
    return !nameError &&
    !dobError &&
    !emailError &&
    !phoneNumberError &&
    !passwordError &&
    !confirmPasswordError &&
    name &&
    dob &&
    email &&
    phoneNumber &&
    password &&
    confirmPassword;
  }


  const togglePasswordVisibility = () => setPasswordVisible(!passwordVisible);
  const toggleConfirmPasswordVisibility = () =>
    setConfirmPasswordVisible(!confirmpasswordVisible);

  
  const validateName = () => {
    if (!name) {
      setNameError("Name is required");
    } else if (!/^[A-Za-z\s]+$/.test(name)) {
      setNameError("Please enter only alphabetic characters");
    } else {
      setNameError("");
    }
  };

  const validateEmail = () => {
    if (!email) {
      setEmailError("Email ID is required");
    } else if (!/\S+@\S+\.\S+/.test(email)) {
      setEmailError("Please enter a valid email address");
    } else {
      setEmailError("");
    }
  };
  const validateDob = () => {
    if (!dob) {
      setDobError("Date of birth is required");
    } else {
      var currentDate = new Date();
      var dobDate = new Date(dob);
      var age = currentDate.getFullYear() - dobDate.getFullYear();
      if (dobDate > currentDate) {
        setDobError("Future date is not allowed");}
      else{
      setDobError(
        age < 18 ? "You should be at least 18 years old to register" : ""
      );
    }
    }
  };

  const validatePhoneNumber = () => {
    if(!phoneNumber){
      setPhoneNumberError("Phone number is required")
    }
    else if(!/^\d{10}$/.test(phoneNumber)){
      setPhoneNumberError("Please enter a valid phone number of 10 digits")
    }
  };

  const validatePassword = () => {
    if (!password) {
      setPasswordError("Password is required");
    } else if (password.length < 8) {
      setPasswordError("Password should have at least 8 characters");
    } else if (!/[a-z]/.test(password)) {
      setPasswordError("Password should have at least one lowercase letter");
    } else if (!/[A-Z]/.test(password)) {
      setPasswordError("Password should have at least one uppercase letter");
    } else if (!/[0-9]/.test(password)) {
      setPasswordError("Password should have at least one number");
    } else if (!/[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(password)) {
      setPasswordError("Password should have at least one special character");
    } else {
      setPasswordError("");
    }
  };

  const validateConfirmPassword = () => {
    if(!confirmPassword){
      setConfirmPasswordError("Password confirmation is required");
    }
    else if(password !== confirmPassword)
    {
      setConfirmPasswordError("Passwords do not match");
    }
  };

  const handleSignUp = async () => {
    validateName();
    validateDob();
    validateEmail();
    validatePhoneNumber();
    validatePassword();
    validateConfirmPassword();
    if (
      isFormValid()
    ) {
      try {
        const response = await fetch(
          process.env.REACT_APP_lOGINWATCHLIST_API+"User/Registration",
          {
            method: "POST",
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
            },
            body: JSON.stringify({ ...user, user: {} }),
          }
        );

        if (response.status === 201) {
          const data = await response.json();
          toast.success("Successfully registered");
          localStorage.setItem("token", data.token.toString());
          localStorage.setItem("emailId", data.emailId.toString());
          localStorage.setItem("userId", data.userId);
          navigate("/dashboard");
        }  else if (response.status === 400) {
          var errorData = await response.text(); 
          toast.error(errorData);
        }
      } catch (error) {
        console.error(error);
      }
    }
  };
  const navigateToSignIn = () => {
    navigate("/Authentication");
  };

  return (
    <div className="SignIn">
      <div className="SignUpCredentials">
        <div className="SignInContext">
          <span className="SignInLable">Sign Up</span>
          <span className="SignInLableSubText">
            Welcome! Please enter your details for signing up.
          </span>
        </div>
        <div className="SignInContext">
          <label className="SigInInputLable">Name</label>
          <input
            type="text"
            className="SigInInputField"
            placeholder="Eg: Jhon"
            value={name}
            onChange={(e) => {
              setName(e.target.value);
              setNameError("");
              setUser({
                ...user,
                userInfo: {
                  ...user.userInfo,
                  name: e.target.value,
                },
              });
            }}
            onBlur={validateName}
          />
          {nameError && <span className="error-red-text">{nameError}</span>}
        </div>

        <div className="SignInContext">
          <label className="SigInInputLable">Date Of Birth</label>
          <input
            type="date"
            className="SigInInputField"
            value={dob}
            onChange={(e) => {
              setDob(e.target.value);
              setDobError("");
              setUser({
                ...user,
                userInfo: {
                  ...user.userInfo,
                  dateOfBirth: e.target.value,
                },
              });
            }}
            onBlur={validateDob}
          />
          {dobError && <span className="error-red-text">{dobError}</span>}
        </div>

        <div className="SignInContext">
          <label className="SigInInputLable">Phone Number</label>
          <input
            type="tel"
            className="SigInInputField"
            placeholder="Eg: 9876543210"
            value={phoneNumber}
            onChange={(e) => {
              const numericValue = e.target.value.replace(/\D/g, "");
              setPhoneNumber(e.target.value);
              setPhoneNumberError("");
              setUser({
                ...user,
                userInfo: {
                  ...user.userInfo,
                  phoneNumber: e.target.value,
                },
              });
            }}
            onBlur={validatePhoneNumber}
          />
          {phoneNumberError && (
            <span className="error-red-text">{phoneNumberError}</span>
          )}
        </div>

        <div className="SignInContext">
          <label className="SigInInputLable">Email</label>
          <input
            type="email"
            className="SigInInputField"
            placeholder="Eg: john@kanini.com"
            value={email}
            onChange={(e) => {
              setEmail(e.target.value);
              setEmailError("");
              setUser({
                ...user,
                emailId: e.target.value,
              });
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
              }}
              onBlur={validatePassword}
            />
            <span
              className="password-toggle"
              onClick={togglePasswordVisibility}
            >
              {passwordVisible ? (
                <i className="bi bi-eye-slash-fill PasswordVisible"></i>
              ) : (
                <i className="bi bi-eye-fill PasswordVisible"></i>
              )}
            </span>
          </div>
          {passwordError && (
            <span className="error-red-text">{passwordError}</span>
          )}
        </div>

        <div className="SignInContext">
          <label className="SigInInputLable">Confirm Password</label>
          <div className="password-input">
            <input
              type={confirmpasswordVisible ? "text" : "password"}
              placeholder="Password"
              className="SigInInputField"
              value={confirmPassword}
              onChange={(e) => {
                setConfirmPassword(e.target.value);
                setConfirmPasswordError("");
                setUser({
                  ...user,
                  passwordClear: e.target.value,
                });
              }}
              onBlur={validateConfirmPassword}
            />
            <span
              className="password-toggle"
              onClick={toggleConfirmPasswordVisibility}
            >
              {confirmpasswordVisible ? (
                <i className="bi bi-eye-slash-fill PasswordVisible"></i>
              ) : (
                <i className="bi bi-eye-fill PasswordVisible"></i>
              )}
            </span>
          </div>{" "}
          {confirmPasswordError && (
            <span className="error-red-text">{confirmPasswordError}</span>
          )}
        </div>
      </div>

      <div className="SignUpButtonContainer">
        <button
          className={`SignUpButton ${
            !isFormValid() ? "disabled" : ""
          }`}
          onClick={handleSignUp}
          disabled={
            !isFormValid()
          }
        >
          SIGN UP
        </button>
        <div className="SignUpLink">
          <span>
            Already a user? <span id="SignInLinkClickHere" onClick={navigateToSignIn}>Sign In</span>
          </span>
        </div>
      </div>
    </div>
  );
}

export default SignUp;