using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.Services;
using Moq;

namespace TestLoginWatchList.TestWatchListService
{
    [TestClass]
    public class TestRemovingFromWatchList
    {
        #region Properties
        private Mock<IRepository<int, User>> _userRepositoryMock=null!;
        private Mock<IRepository<int, WatchList>> _watchListRepositoryMock=null!;
        private WatchListService _watchListService = null!;
        #endregion

        #region TestInitialize
        [TestInitialize]
        public void Initialize()
        {
            // Initialize the mock repositories
            _userRepositoryMock = new Mock<IRepository<int, User>>();
            _watchListRepositoryMock = new Mock<IRepository<int, WatchList>>();
            _watchListService = new WatchListService(_watchListRepositoryMock.Object, _userRepositoryMock.Object);
        }
        #endregion

        #region TestMethod RemovingFromWatchListValidInputReturnsWatchList
        [TestMethod]
        public async Task RemovingFromWatchListValidInputReturnsWatchList()
        {
            // Arrange
            var watchListToDelete = new WatchList { UserId = 1, CompanyId = 1, WatchListId = 1 };
            _watchListRepositoryMock!.Setup(repo => repo.GetAll()).ReturnsAsync(new[] { watchListToDelete });
            _watchListRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(watchListToDelete);

            // Act
            var result = await _watchListService!.RemovingFromWatchList(new GetWatchListIdDTO { UserId = 1, CompanyId = 1 });

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion

        #region TestMethod RemovingFromWatchListErrorDeletingThrowsCustomException
        [TestMethod]
        public async Task RemovingFromWatchListErrorDeletingThrowsCustomException()
        {
            // Arrange
            var watchListToDelete = new WatchList { UserId = 1, CompanyId = 1, WatchListId = 1 };
            _watchListRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new[] { watchListToDelete });
            // Act
             _watchListRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync((WatchList?)null);
            // Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _watchListService.RemovingFromWatchList(new GetWatchListIdDTO { UserId = 1, CompanyId = 1 }));
        }
        #endregion

        #region TestMethod  RemovingFromWatchListNoDataFoundThrowsCustomException
        [TestMethod]
        public async Task RemovingFromWatchListNoDataFoundThrowsCustomException()
        {
            // Arrange
            _watchListRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<WatchList>());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _watchListService.RemovingFromWatchList(new GetWatchListIdDTO()), "your wishlist is empty");
        }
        #endregion

        #region TestMethod RemovingFromWatchListWatchlistEmpty_hrowsCustomException
        [TestMethod]
        public async Task RemovingFromWatchListWatchlistEmpty_hrowsCustomException()
        {
            // Arrange
            _watchListRepositoryMock!.Setup(repo => repo.GetAll()).ReturnsAsync(new List<WatchList>());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _watchListService!.RemovingFromWatchList(new GetWatchListIdDTO()));
        }
        #endregion


    }
}