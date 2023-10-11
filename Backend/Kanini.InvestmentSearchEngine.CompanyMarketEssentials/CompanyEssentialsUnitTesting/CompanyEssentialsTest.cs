#nullable disable 
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Interfaces;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Models;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Services;
using Moq;

namespace CompanyEssentialsUnitTesting
{
    [TestClass] 
    public class CompanyEssentialsTest
    {
        private CompanyEssentialsServices _companyEssentialsService;
        private Mock<IRepository<int, CompanyEssentials>> _companyEssentialsRepo;

        [TestInitialize]
        public void Initialize()
        {
            _companyEssentialsRepo = new Mock<IRepository<int, CompanyEssentials>>();
            _companyEssentialsService = new CompanyEssentialsServices(_companyEssentialsRepo.Object);
        }
        #region Test Cases for Get Essentials
        [TestMethod]
        public async Task GetEssential_ExistingKey_ReturnsCompanyEssentials()
        {
            int keyToGet = 1;
            var expectedCompanyEssentials = new CompanyEssentials
            {
                EssenID = keyToGet,
                CompanyID = 101,
                MarketCap = 1000000,
                EnterpriceValue = 500000,
                NoOfShares = 100000,
                DivYield = 0.05,
                Cash = 250000,
                PromoterHolding = 60,
                Price = 50,
                BookValue = 40,
                PriceToBook = 1.25,
                PriceToEarning = 10,
                Eps = 5,
                NetIncome = 500000,
                Sector = "Technology",
            };

            _companyEssentialsRepo.Setup(repo => repo.Get(keyToGet))
                .ReturnsAsync(expectedCompanyEssentials);

            var result = await _companyEssentialsService.GetEssential(keyToGet);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyToGet, result.EssenID);
        }


        [TestMethod]
        public async Task GetEssential_NonExistentKey_ReturnsNull()
        {
            int keyToGet = 999;

            _companyEssentialsRepo.Setup(repo => repo.Get(keyToGet))
                .ReturnsAsync((CompanyEssentials)null);

            var result = await _companyEssentialsService.GetEssential(keyToGet);

            Assert.IsNull(result);
        }
        #endregion

        #region Test Cases for Delete Essentials
        [TestMethod]
        public async Task DeleteEssential_ExistingId_ReturnsDeletedCompanyEssentials()
        {
            var companyIdToDelete = 1;

            var deletedCompanyEssentials = new CompanyEssentials
            {
                EssenID = companyIdToDelete,
                CompanyID = 101,
                MarketCap = 1000000,
                EnterpriceValue = 500000,
                NoOfShares = 100000,
                DivYield = 0.05,
                Cash = 250000,
                PromoterHolding = 60,
                Price = 50,
                BookValue = 40,
                PriceToBook = 1.25,
                PriceToEarning = 10,
                Eps = 5,
                NetIncome = 500000,
                Sector = "Technology",
            };

            _companyEssentialsRepo.Setup(repo => repo.Delete(companyIdToDelete))
                .ReturnsAsync(deletedCompanyEssentials);

            var result = await _companyEssentialsService.DeleteEssential(companyIdToDelete);

            Assert.AreEqual(deletedCompanyEssentials, result);
        }

        [TestMethod]
        public async Task Delete_NonExistentKey_ReturnsNull()
        {
            var keyToDelete = 123;
            var mockRepository = new Mock<IRepository<int, CompanyEssentials>>();
            mockRepository.Setup(repo => repo.Delete(keyToDelete)).ReturnsAsync((CompanyEssentials)null);

            var repositoryUnderTest = mockRepository.Object;

            var result = await repositoryUnderTest.Delete(keyToDelete);

            Assert.IsNull(result);
        }
        #endregion

        #region Test cases for Update Essentials
        [TestMethod]
        public async Task UpdateEssential_ValidData_ReturnsUpdatedCompanyEssentials()
        {
            var updatedCompanyEssentials = new CompanyEssentials
            {
                EssenID = 1,
                CompanyID = 101,
                MarketCap = 1000000,
                EnterpriceValue = 500000,
                NoOfShares = 100000,
                DivYield = 0.05,
                Cash = 250000,
                PromoterHolding = 60,
                Price = 50,
                BookValue = 40,
                PriceToBook = 1.25,
                PriceToEarning = 10,
                Eps = 5,
                NetIncome = 500000,
                Sector = "Technology",
            };

            _companyEssentialsRepo.Setup(repo => repo.Update(It.IsAny<CompanyEssentials>()))
                .ReturnsAsync(updatedCompanyEssentials);

            var result = await _companyEssentialsService.UpdateEssential(updatedCompanyEssentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(updatedCompanyEssentials, result);
        }

        [TestMethod]
        public async Task UpdateEssential_FailedUpdate_ReturnsNull()
        {
            var UpdatedEssentials = new CompanyEssentials
            {
                EssenID = 1,
                CompanyID = 101,
                MarketCap = 1000000,
                EnterpriceValue = 500000,
                NoOfShares = 100000,
                DivYield = 0.05,
                Cash = 250000,
                PromoterHolding = 60,
                Price = 50,
                BookValue = 40,
                PriceToBook = 1.25,
                PriceToEarning = 10,
                Eps = 5,
                NetIncome = 500000,
                Sector = "Technology",

            };
            _companyEssentialsRepo.Setup(repo => repo.Update(It.IsAny<CompanyEssentials>()))
                .ReturnsAsync((CompanyEssentials)null);
            var result = await _companyEssentialsService.UpdateEssential(UpdatedEssentials);
            Assert.IsNull(result);
        }
        #endregion

        #region Test Cases for Filtered Companies
        [TestMethod]
        public async Task FilterCompanies_ReturnsFilteredCompanies()
        {
            var trendingService = new CompanyEssentialsServices(_companyEssentialsRepo.Object);

            var companies = new List<CompanyEssentials>
            {
                new CompanyEssentials { EssenID = 1, Sector = "Tech", PriceToEarning = 20, PriceToBook = 30, DivYield = 0.1 },
                new CompanyEssentials { EssenID = 2, Sector = "Finance", PriceToEarning = 30, PriceToBook = 15, DivYield = 0.05 },
                new CompanyEssentials { EssenID = 3, Sector = "Tech", PriceToEarning = 15, PriceToBook = 40, DivYield = 0.2 },
                new CompanyEssentials { EssenID = 4, Sector = "Finance", PriceToEarning = 25, PriceToBook = 10, DivYield = 0.03 },
                new CompanyEssentials { EssenID = 5, Sector = "Health", PriceToEarning = 18, PriceToBook = 35, DivYield = 0.15 }
            };

            _companyEssentialsRepo.Setup(repo => repo.GetAll()).ReturnsAsync(companies);

            var result = await trendingService.FilterCompanies();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
        #endregion

        #region Test Cases for GetAll Essentials
        [TestMethod]
        public async Task GetAllEssentials_ReturnsListOfCompanyEssentials()
        {
            var expectedEssentialsList = new List<CompanyEssentials>
        {
            new CompanyEssentials
            {
                EssenID = 1,
                CompanyID = 101,
                MarketCap = 1000000,
                EnterpriceValue = 500000,
                NoOfShares = 100000,
                DivYield = 0.05,
                Cash = 250000,
                PromoterHolding = 60,
                Price = 50,
                BookValue = 40,
                PriceToBook = 1.25,
                PriceToEarning = 10,
                Eps = 5,
                NetIncome = 500000,
                Sector = "Technology",
            },
            new CompanyEssentials
            {
                EssenID = 2,
                CompanyID = 101,
                MarketCap = 1000000,
                EnterpriceValue = 500000,
                NoOfShares = 100000,
                DivYield = 0.05,
                Cash = 250000,
                PromoterHolding = 60,
                Price = 50,
                BookValue = 40,
                PriceToBook = 1.25,
                PriceToEarning = 10,
                Eps = 5,
                NetIncome = 500000,
                Sector = "Technology"
            }
        };

            _companyEssentialsRepo.Setup(repo => repo.GetAll())
                .ReturnsAsync(expectedEssentialsList);

            var result = await _companyEssentialsService.GetAllEssentials();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedEssentialsList.ToList(), result.ToList());
        }
        [TestMethod]
        public async Task GetAllEssentials_ReturnsEmptyListWhenNoData()
        {
            var emptyList = new List<CompanyEssentials>();

            _companyEssentialsRepo.Setup(repo => repo.GetAll())
                .ReturnsAsync(emptyList);

            var result = await _companyEssentialsService.GetAllEssentials();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
        #endregion

        #region Test Case for AddEssentials
        [TestMethod]
        public async Task AddEssentials_SuccessfulAdd_ReturnsAddedCompanyEssentials()
        {
            var newItem = new CompanyEssentials
            {
                CompanyID = 104,
                MarketCap = 1000000,
                EnterpriceValue = 500000,
                NoOfShares = 100000,
                DivYield = 0.05,
                Cash = 250000,
                PromoterHolding = 60,
                Price = 50,
                BookValue = 40,
                PriceToBook = 1.25,
                PriceToEarning = 10,
                Eps = 5,
                NetIncome = 500000,
                Sector = "Technology",
            };

            _companyEssentialsRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new List<CompanyEssentials>());
            _companyEssentialsRepo.Setup(repo => repo.Add(newItem)).ReturnsAsync(newItem);

            var result = await _companyEssentialsService.AddEssentials(newItem);

            Assert.IsNotNull(result);
            Assert.AreEqual(newItem.CompanyID, result.CompanyID);
        }
        [TestMethod]
        public async Task AddEssentials_UnsuccessfulAdd_ReturnsNull()
        {
            var newItem = new CompanyEssentials
            {
                CompanyID = 1,
                MarketCap = 1000000,
                EnterpriceValue = 500000,
                NoOfShares = 100000,
                DivYield = 0.05,
                Cash = 250000,
                PromoterHolding = 60,
                Price = 50,
                BookValue = 40,
                PriceToBook = 1.25,
                PriceToEarning = 10,
                Eps = 5,
                NetIncome = 500000,
                Sector = "Technology",
            };

            _companyEssentialsRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new List<CompanyEssentials>());
            _companyEssentialsRepo.Setup(repo => repo.Add(newItem)).ReturnsAsync((CompanyEssentials)null);

            var result = await _companyEssentialsService.AddEssentials(newItem);

            Assert.IsNull(result);
        }
        #endregion

    }
}
