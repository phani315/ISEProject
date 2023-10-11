using Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Services;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Exceptions;
using Moq;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Mapper;

namespace TestSWOTService
{
    [TestClass]
    public class TestSWOTService
    {
        private readonly Mock<IRepository<int, SWOT>> _repoMock;
        private readonly SWOTService _serviceMock;
        private readonly IMapper _mapperMock;

        public TestSWOTService()
        {
            _repoMock = new Mock<IRepository<int, SWOT>>();
            _mapperMock = new SwotTOSwotDto();
            _serviceMock = new SWOTService(_repoMock.Object, _mapperMock);
        }


        [TestMethod]
        public async Task Update_SWOT_ShouldReturns_SWOTObject()
        {
            //Assign
            var swotData = new SWOT
            {
                SwotId = 1,
                CompanyID = 123,
                Strength = null,
                Weakness = null,
                Threat = null,
                Oppurtunity = null,
                Date = DateTime.Now
            };

            _repoMock.Setup(r => r.Update(swotData)).ReturnsAsync(swotData);

            //Act
            var result = await _serviceMock.UpdateSwotDetails(swotData);


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyID, swotData.CompanyID);
        }


        [TestMethod]
        public async Task Update_SWOT_ShouldReturns_Exception()
        {
            //Assign
            var swotData = new SWOT
            {
                SwotId = 1,
                CompanyID = 123,
                Strength = null,
                Weakness = null,
                Threat = null,
                Oppurtunity = null,
                Date = DateTime.Now
            };

            SWOT updatedData = null;

            _repoMock.Setup(r => r.Update(swotData)).ReturnsAsync(updatedData);

            //Act & Assert

            await Assert.ThrowsExceptionAsync<NullSWOTDetailsException>(() => _serviceMock.UpdateSwotDetails(swotData));
        }

        [TestMethod]
        public async Task Add_SWOT_ShouldReturns_SWOTObject()
        {
            // Assign
            var swotData = new SWOT
            {
                SwotId = 1,
                CompanyID = 123,
                Strength = null,
                Weakness = null,
                Threat = null,
                Oppurtunity = null,
                Date = DateTime.Now
            };

            _repoMock.Setup(r => r.Add(swotData)).ReturnsAsync(swotData);

            // Act
            var result = await _serviceMock.AddSwotDetails(swotData);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyID, swotData.CompanyID);
        }

        [TestMethod]
        public async Task Delete_SWOT_ShouldReturns_SWOTObject()
        {
            // Assign
            int swotId = 1;
            var swotData = new SWOT
            {
                SwotId = swotId,
                CompanyID = 123,
                Strength = null,
                Weakness = null,
                Threat = null,
                Oppurtunity = null,
                Date = DateTime.Now
            };

            _repoMock.Setup(r => r.Delete(swotId)).ReturnsAsync(swotData);

            // Act
            var result = await _serviceMock.DeleteSwotDeatils(swotId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyID, swotData.CompanyID);
        }

        [TestMethod]
        public async Task Delete_SWOT_ShouldReturns_Exception()
        {
            // Assign
            int swotId = 1;

            _repoMock.Setup(r => r.Delete(swotId)).ReturnsAsync((SWOT)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NullSWOTDetailsException>(() => _serviceMock.DeleteSwotDeatils(swotId));
        }

        [TestMethod]
        public async Task Get_SWOT_ShouldReturns_SWOTObject()
        {
            // Assign
            int companyID = 1;
            var swotData = new SWOT
            {
                SwotId = 1,
                CompanyID = companyID,
                Strength = new Strength { StrengthId = 1, StrengthValue = 1, StrengthDescription = "Finance" },
                Weakness = new Weakness { WeaknessId = 1, WeaknessValue = 1, WeaknessDescription = "Ownership" },
                Threat = new Threat { ThreatId = 1, ThreatValue = 1, ThreatDescription = "Market cap" },
                Oppurtunity = new Oppurtunity { OppurtunityId = 1, OppurtunityValue = 1, OppurtunityDescription = "Earnings" },
                Date = DateTime.Now
            };

            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(new List<SWOT> { swotData });

            // Act
            var result = await _serviceMock.GetSWOTDetailsByCompanyID(companyID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyID, swotData.CompanyID);
        }

        [TestMethod]
        public async Task Get_SWOT_ShouldReturns_Exception()
        {
            // Assign
            int companyID = 1;

            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(new List<SWOT>());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NullCompanyDetailsException>(() => _serviceMock.GetSWOTDetailsByCompanyID(companyID));
        }

        [TestMethod]
        public async Task GetAll_SWOT_ShouldReturns_SWOTObjects()
        {
            // Assign
            var swotDataList = new List<SWOT>
            {
                new SWOT { SwotId = 1, CompanyID = 123, Strength = null, Weakness = null, Threat = null, Oppurtunity = null, Date = DateTime.Now },
                new SWOT { SwotId = 2, CompanyID = 456, Strength = null, Weakness = null, Threat = null, Oppurtunity = null, Date = DateTime.Now }
            };

            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(swotDataList);

            // Act
            var result = await _serviceMock.GetAllSwotDetails();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, swotDataList.Count);
        }

        [TestMethod]
        public async Task GetAll_SWOT_ShouldReturns_Null()
        {
            // Assign
            _repoMock.Setup(r => r.GetAll()).ReturnsAsync((List<SWOT>)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NullSWOTDetailsException>(() => _serviceMock.GetAllSwotDetails());



        }

    }
}