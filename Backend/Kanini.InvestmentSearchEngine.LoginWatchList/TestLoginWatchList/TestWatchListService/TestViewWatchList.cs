using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.Services;
using Moq;


namespace TestLoginWatchList.TestWatchListService
{

    [TestClass]
    public class TestViewWatchList
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
            // Initialize the mock
            _userRepositoryMock = new Mock<IRepository<int, User>>();
            _watchListRepositoryMock = new Mock<IRepository<int, WatchList>>();
            _watchListService = new WatchListService(_watchListRepositoryMock.Object, _userRepositoryMock.Object);
        }
        #endregion

        #region TestMethod ViewWatchListUserHasWatchlistReturnsWatchlist
        [TestMethod]
        public async Task ViewWatchListUserHasWatchlistReturnsWatchlist()
        {
            // Arrange
            var userId = 1;
            var watchListUserIdDTO = new WatchListUserIdDTO
            {
                UserId = userId
            };
            var watchListCollection = new List<WatchList>
            {
              new WatchList
              {
                WatchListId = 1,
                UserId = userId,
                CompanyId = 1
              },
              new WatchList
              {
                WatchListId = 2,
                UserId = userId,
                CompanyId = 2
              }
            };
            _watchListRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(watchListCollection);
            // Act
            var result = await _watchListService.ViewWatchList(watchListUserIdDTO);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
        #endregion



        #region TestMethod ViewWatchListThrowsCustomExceptionWhenEmpty
        [TestMethod]
        public async Task ViewWatchListThrowsCustomExceptionWhenEmpty()
        {
            // Arrange
            var watchListUserIdDTO = new WatchListUserIdDTO
            {
                UserId = 1 // You can use any valid user ID here
            };

             _watchListRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync((List<WatchList>)null);

            // Act and Assert
            CustomException exception = await Assert.ThrowsExceptionAsync<CustomException>(() =>
                _watchListService.ViewWatchList(watchListUserIdDTO));

            // Assert the exception message if needed
            Assert.AreEqual("no items in watchlist", exception.Message);
        }


        #endregion
    }
}
