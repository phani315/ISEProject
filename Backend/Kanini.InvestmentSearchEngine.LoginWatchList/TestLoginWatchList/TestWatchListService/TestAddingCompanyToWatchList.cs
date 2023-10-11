using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.Services;
using Moq;

namespace TestLoginWatchList.TestAddingCompanyToWatchList
{
    [TestClass]
    public class TestAddingCompanyToWatchList
    {
        #region Properties
        private Mock<IRepository<int, User>> _userRepositoryMock = null!;
        private Mock<IRepository<int, WatchList>> _watchListRepositoryMock = null!;
        private WatchListService _watchListService = null!;
        #endregion

        #region TestInitialize
        [TestInitialize]
        public void Initialize()
        {
            _userRepositoryMock = new Mock<IRepository<int, User>>();
            _watchListRepositoryMock = new Mock<IRepository<int, WatchList>>();
            _watchListService = new WatchListService(_watchListRepositoryMock.Object, _userRepositoryMock.Object);
        }
        #endregion

        #region TestMethod AddCompanyToWatchListValidInputReturnsWatchList

        [TestMethod]
        public async Task AddCompanyToWatchListValidInputReturnsWatchList()
        {
            // Arrange
            var userId = 1;
            var companyId = 1;
            var watchList = new WatchList
            {
                UserId = userId,
                CompanyId = companyId
            };
            // Act
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(new User());
            _watchListRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync((List<WatchList>?)null);
            _watchListRepositoryMock.Setup(repo => repo.Add(It.IsAny<WatchList>())).ReturnsAsync(watchList);
            var result = await _watchListService.AddCompanyToWatchList(watchList);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(companyId, result.CompanyId);
        }
        #endregion

        #region TestMethod AddCompanyToWatchListUserNotExistsThrowsCustomException

        [TestMethod]
        public async Task AddCompanyToWatchListUserNotExistsThrowsCustomException()
        {
            // Arrange
            var userId = 1;
            var companyId = 1;
            var watchList = new WatchList
            {
                UserId = userId,
                CompanyId = companyId
            };
            // Act
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync((User?)null);
            // Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _watchListService.AddCompanyToWatchList(watchList));
        }
        #endregion

        #region TestMethod AddCompanyToWatchListCompanyAlreadyInWatchlistThrowsCustomException
        [TestMethod]
        public async Task AddCompanyToWatchListCompanyAlreadyInWatchlistThrowsCustomException()
        {
            // Arrange
            var userId = 1;
            var companyId = 1;
            var watchList = new WatchList
            {
                UserId = userId,
                CompanyId = companyId
            };

            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(new User());

            var existingWatchList = new List<WatchList>
            {
            new WatchList
            {
                   UserId = userId,
                   CompanyId = companyId
            }
            };
            // Act 
            _watchListRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(existingWatchList);

            //  Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _watchListService.AddCompanyToWatchList(watchList));
        }
        #endregion

        #region TestMethod AddCompanyToWatchListUnableToAddThrowsCustomException
        [TestMethod]
        public async Task AddCompanyToWatchListUnableToAddThrowsCustomException()
        {
            // Arrange
            var userId = 1;
            var companyId = 1;
            var watchList = new WatchList
            {
                UserId = userId,
                CompanyId = companyId
            };
            // Act 
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(new User());
            _watchListRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync((List<WatchList>?)null);
            _watchListRepositoryMock.Setup(repo => repo.Add(watchList)).ReturnsAsync((WatchList?)null);

            //  Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _watchListService.AddCompanyToWatchList(watchList));
        }
        #endregion


    }
}
