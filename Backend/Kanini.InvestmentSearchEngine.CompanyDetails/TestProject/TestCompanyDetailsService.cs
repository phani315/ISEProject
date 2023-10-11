using Kanini.InvestmentSearchEngine.CompanyDetails.Exceptions;
using Kanini.InvestmentSearchEngine.CompanyDetails.Interfaces;
using Kanini.InvestmentSearchEngine.CompanyDetails.Models;
using Kanini.InvestmentSearchEngine.CompanyDetails.Services;
using Moq;

namespace TestProject
{
    [TestClass]
    public class TestCompanyDetailsService
    {
        private readonly Mock<IRepo<int, CompanyDetail>> _repoMock;
        private readonly CompanyDetailsServices _serviceMock;

        public TestCompanyDetailsService()
        {
            _repoMock = new Mock<IRepo<int, CompanyDetail>>();
            _serviceMock = new CompanyDetailsServices(_repoMock.Object);
        }
        [TestMethod]
        public async Task Update_CompanyDetails_ShouldReturns_Object()
        {
            //Assign
            var companyDetatilsData = new CompanyDetail
            {
                CompanyId = 1,
                CompanyName = "Nestle",
                NSE = "FOOD788",
                BSE ="1234",
                Sector = "FOOD",
                Image = "google.drive"               
            };

            _repoMock.Setup(r => r.Update(companyDetatilsData)).ReturnsAsync(companyDetatilsData);

            //Act
            var result = await _serviceMock.UpdateCompanyDetails(companyDetatilsData);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyId, companyDetatilsData.CompanyId);
        }

        [TestMethod]
        public async Task Update_CompanyDetails_ShouldReturns_DataNotFoundException()
        {
            //Assign
            var companyDetatilsData = new CompanyDetail
            {
                CompanyId = 1,
                CompanyName = "Nestle",
                NSE = "FOOD788",
                BSE = "1234",
                Sector = "FOOD",
                Image = "google.drive"
            };

            CompanyDetail updatedData = null;
            _repoMock.Setup(r => r.Update(companyDetatilsData)).ReturnsAsync(updatedData);

            //Act & Assert

            await Assert.ThrowsExceptionAsync<DataNotFoundException>(() => _serviceMock.UpdateCompanyDetails(companyDetatilsData));
        }
        [TestMethod]
        public async Task Add_CompanyDetails_ShouldReturns_CompanyDetailsObject()
        {
            //Assign
            var companyDetatilsData = new CompanyDetail
            {
                CompanyId = 1,
                CompanyName = "Nestle",
                NSE = "FOOD788",
                BSE = "1234",
                Sector = "FOOD",
                Image = "google.drive"
            };
            _repoMock.Setup(r => r.Add(companyDetatilsData)).ReturnsAsync(companyDetatilsData);

            //Act
            var result = await _serviceMock.AddCompanyDetails(companyDetatilsData);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyId, companyDetatilsData.CompanyId);
        }

        [TestMethod]
        public async Task Add_CompanyDetails_ShouldReturns_DataNotFoundException()
        {
            //Assign
            var companyDetatilsData = new CompanyDetail
            {
                CompanyId = 1,
                CompanyName = "Nestle",
                NSE = "FOOD788",
                BSE = "1234",
                Sector = "FOOD",
                Image = "google.drive"
            };

            CompanyDetail updatedData = null;
            _repoMock.Setup(r => r.Add(companyDetatilsData)).ReturnsAsync(updatedData);

            //Act & Assert

            await Assert.ThrowsExceptionAsync<DataNotFoundException>(() => _serviceMock.AddCompanyDetails(companyDetatilsData));
        }
        [TestMethod]
        public async Task Delete_CompanyDetails_ShouldReturns_CompanyDetailsObject()
        {
            //Assign
            int companyId = 1;
            var companyDetatilsData = new CompanyDetail
            {
                CompanyId = 1,
                CompanyName = "Nestle",
                NSE = "FOOD788",
                BSE = "1234",
                Sector = "FOOD",
                Image = "google.drive"
            };
            _repoMock.Setup(r => r.Delete(companyId)).ReturnsAsync(companyDetatilsData);

            //Act
            var result = await _serviceMock.DeleteCompanyDetails(companyId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyId, companyDetatilsData.CompanyId);
        }
        [TestMethod]
        public async Task Delete_CompanyDetails_ShouldReturns_Exception()
        {
            // Assign
            int companyId = 1;

            _repoMock.Setup(r => r.Delete(companyId)).ReturnsAsync((CompanyDetail)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<DataNotFoundException>(() => _serviceMock.DeleteCompanyDetails(companyId));
        }

        [TestMethod]
        public async Task Get_CompanyDetails_ShouldReturns_CompanyDetailsObject()
        {
            //Assign
            int companyId = 1;
            var companyDetatilsData = new CompanyDetail
            {
                CompanyId = 1,
                CompanyName = "Nestle",
                NSE = "FOOD788",
                BSE = "1234",
                Sector = "FOOD",
                Image = "google.drive"
            };
            _repoMock.Setup(r => r.Get(companyId)).ReturnsAsync(companyDetatilsData);

            //Act
            var result = await _serviceMock.GetCompanyDetailsById(companyId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyId, companyDetatilsData.CompanyId);
        }

        [TestMethod]
        public async Task Get_CompanyDetails_ShouldReturns_Exception()
        {
            // Assign
            int companyId = 1;

            _repoMock.Setup(r => r.Get(companyId)).ReturnsAsync((CompanyDetail)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<DataNotFoundException>(() => _serviceMock.GetCompanyDetailsById(companyId));
        }

        [TestMethod]
        public async Task GetAll_CompanyDetails_ShouldReturns_CompanyDetailsObjects()
        {
            //Assign
            var companyDetatilsData = new List<CompanyDetail>
            {
                new CompanyDetail
                {
                    CompanyId = 1,
                    CompanyName = "Nestle",
                    NSE = "FOOD788",
                    BSE = "1234",
                    Sector = "FOOD",
                    Image = "google.drive"
                },
                new CompanyDetail
                {
                    CompanyId = 2,
                    CompanyName = "Apple",
                    NSE = "IT788",
                    BSE = "1244",
                    Sector = "IT",
                    Image = "google.drive"
                }
            };
            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(companyDetatilsData);

            //Act
            var result = await _serviceMock.GetAllCompanyDetails();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count,companyDetatilsData.Count);
        }
        [TestMethod]
        public async Task GetAll_CompanyDetails_ShouldReturns_Null()
        {
            // Assign
            _repoMock.Setup(r => r.GetAll()).ReturnsAsync((List<CompanyDetail>)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<DataNotFoundException>(() => _serviceMock.GetAllCompanyDetails());

        }

    }
}