import React from "react";
import "./Home.css";
import HomeBannerBottomImage from "../../../Assets/Images/Home/HomeBannerCenter.png";
import DiscoverImage1 from "../../../Assets/Images/Home/DiscoverImage1.png";
import DiscoverImage2 from "../../../Assets/Images/Home/DiscoverImage2.png";
import DiscoverImage3 from "../../../Assets/Images/Home/DiscoverImage3.png";
import { useNavigate } from "react-router";

function Home() {
  const navigate = useNavigate();
  return (
    <div className="Home">
      <div className="HomeBanner">
       
        <div className="HomeBannerTitleContainer">
          <span className="HomeBannerTitle">How to invest in stockmarket</span>
        </div>
        <div className="HomeBannerDescription">
          <div className="HomeBannerDsectext">
            <p>
              Unearth a concise guide to start your investing journey and
              optimize your market opportunities.
            </p>
            <div className="LoginCalculatorContainer">
              <div>
                <button
                  className="LoginToKnowButton"
                  onClick={() => {
                    navigate("/Authentication");
                  }}
                >
                  Login to know
                </button>
              </div>
              <div>
                <button
                  className="CheckCalculatorButton"
                  onClick={() => {
                    navigate("/calculator");
                  }}
                >
                  Check Calculator
                </button>
              </div>
            </div>
          </div>
          <div className="HomeBannerBottomImage">
            <img
              src={HomeBannerBottomImage}
              className="bottom-image"
              alt="Bottom"
            />
          </div>
        </div>
      </div>
      <div className="DiscoverContainer">
        <div className="DiscoverHeader">
          <span>
            Discover about <span>Stock</span>
          </span>
          <p>
            Start investing with the best company in the world, we ensures you
            to get the best stock possible. Find the right company to invest in.
          </p>
        </div>
        <div className="DiscoverDiscriptionContainer">
          <div className="DiscoverDiscriptionImageContainer">
            <img src={DiscoverImage1} className="DiscoverDiscriptionImage" />
          </div>
          <div className="DiscoverDiscriptionContext">
            <h5>How to invest in stock?</h5>
            <p>
              The stock market and investing can be perplexing, often
              misconceived as a form of gambling. However, when comprehended and
              utilized effectively, the stock market has the potential to
              greatly enhance your finances. Here's a concise guide to steer you
              toward a successful start in your investment journey, enabling you
              to maximize its potential.
            </p>
          </div>
        </div>
        <div className="DiscoverDiscriptionContainer">
          <div className="DiscoverDiscriptionContext">
            <h5>who should invest in stock market</h5>
            <p>
              Similar to the essential needs of drinking water and eating,
              growing your wealth through earning and investing is equally
              vital. Merely relying on paychecks isn't sufficient for providing
              a good life for your family. Investing is a necessity, regardless
              of age or income, as it yields fruitful returns and secures your
              financial future.
            </p>
          </div>
          <div className="DiscoverDiscriptionImageContainer">
            <img src={DiscoverImage2} className="DiscoverDiscriptionImage" />
          </div>
        </div>
        <div className="DiscoverDiscriptionContainer">
          <div className="DiscoverDiscriptionImageContainer">
            <img src={DiscoverImage3} className="DiscoverDiscriptionImage" />
          </div>
          <div className="DiscoverDiscriptionContext">
            <h5>How to find right stock to invest in?</h5>
            <p>
              After grasping investment concepts, practice becomes crucial.
              Prior to actual investing, observe the market and analyze stocks
              using accurate information. Present equity research tools often
              lack precise data. Kanini's Investment Search Engine addresses
              this by offering a vast database of listed companies. It's
              tech-powered for precise analyses, aiding you in selecting prime
              stocks.
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Home;
