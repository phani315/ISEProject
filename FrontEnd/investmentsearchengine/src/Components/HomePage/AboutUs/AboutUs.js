import React from "react";
import "./AboutUs.css";
import aboutuspic from "../../../Assets/Images/AboutUs/about-us-pic.jpg"

function AboutUs() {
  return (
    <div className="about-us-body">
      <section className="about-us" id="about-us">
        <div className="about-heading">
          <p>Make <span className="about-heading-span"> investing</span> simple and rewarding for everyone</p>
        </div>
        <div className="about-container">
          <section className="about">
            <div className="about-img">
              <img
                className="about-img-1"
                src={aboutuspic}
                alt=""
              ></img>
            </div>
            <div className="about-content">
              <h5>The Chapters That Write.KANINI Identity.</h5>
              <p>
                We were not born with Digital in mind. But, we anticipated the
                transformation and evolved to be future-ready, dedicating years
                to build expertise in Cloud Computing, AI & Data Analytics,
                Product Engineering, Automation, IoT, and Custom Solutions. The
                journey has led us to be a Trailblazer of Digital 2.0, enabling
                and accelerating Digital Adoption across multiple industries.
              </p>
              <p>
                We achieve this with our diverse team of Practice Consultants,
                Product and Data Engineers, Data Scientists, and Business
                Analysts. They collaborate and improve customer experience,
                reduce the total cost of operations, ensure compliance, and help
                generate additional revenues.
              </p>
            </div>
          </section>
        </div>
        <div className="cards-container">
          <div className="cards">
            <div className="image-section img-one"></div>
            <div className="card-content">
              <h3>Cutting-Edge Tech</h3>
              <p>
                Technology is at the core of our business operations. This
                ensures that our clients get the convenience that they deserve
                while we stay ahead of time.
              </p>
            </div>
          </div>

          <div className="cards">
            <div className="image-section img-two"></div>
            <div className="card-content">
              <h3>Outstanding Team</h3>
              <p>
                Right talent makes a good company great. We do not hire
                employees but talented individuals who maintain the team spirit
                while excelling at their roles.
              </p>
            </div>
          </div>

          <div className="cards">
            <div className="image-section img-three"></div>
            <div className="card-content">
              <h3>Real Transparency</h3>
              <p>
                We are thoroughly unbiased and transparent. We are strictly
                against commission backed advices and recommendations. Your
                benefit is all that matters to us.
              </p>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}

export default AboutUs;