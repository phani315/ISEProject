
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Kanini.InvestmentSearchEngine.LoginWatchList.Services;
using Moq;

namespace TestLoginWatchList.TestUserService
{
    [TestClass]
    public class TestRegister
    {
        #region Properties
        private Mock<IRepository<int, User>> _userRepositoryMock = null!;
        private Mock<IMapper> _mapperMock = null!;
        private UserService _userService = null!;
        #endregion

        #region TestInitialize

        [TestInitialize]
        public void Initialize()
        {
            _userRepositoryMock = new Mock<IRepository<int, User>>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
        }
        #endregion

        # region TestMethod RegisterUserAlreadyExistsThrowsCustomException
        [TestMethod]
        public async Task RegisterUserAlreadyExistsThrowsCustomException()
        {
            // Arrange
            var userRegistrationDTO = new UserRegisterDTO
            {
                EmailId = "Shivkk1122@gmail.com",
                PasswordClear = "Siva@123",
                UserInfo = new UserInfo
                {
                    Name = "John Doe",
                    PhoneNumber = "1234567890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                }
            };

            // Act
            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { new User() });

            //  Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _userService.Register(userRegistrationDTO));
        }
        #endregion

        #region TestMethod RegisterNewUserReturnsUserLoginDTO
        [TestMethod]
        public async Task RegisterNewUserReturnsUserLoginDTO()
        {
            // Arrange
            var userRegistrationDTO = new UserRegisterDTO
            {
                EmailId = "sam112233@gmail.com",
                PasswordClear = "Siva@1234" ,
                UserInfo = new UserInfo
                {
                    Name = "John Doe",
                    PhoneNumber = "1234567890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                }
            };
            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User>());
            var expectedUser = new User
            {
                Id = 1,
                EmailId = "sam112233@gmail.com"
            };
            var expectedUserLoginDTO = new UserLoginDTO
            {
                UserId = expectedUser.Id,
                Token = "This is my token",
                EmailId = expectedUser.EmailId
            };
            _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<UserRegisterDTO>())).ReturnsAsync(expectedUser);
            _mapperMock.Setup(mapper => mapper.UserToUserLoginDTO(expectedUser)).Returns(expectedUserLoginDTO);

            // Act
            var result = await _userService.Register(userRegistrationDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUserLoginDTO.UserId, result.UserId);
            Assert.AreEqual(expectedUserLoginDTO.Token, result.Token);
            Assert.AreEqual(expectedUserLoginDTO.EmailId, result.EmailId);
        }
        #endregion


        #region TestMethod RegisterUnableToRegisterReturnsCustomException
        [TestMethod]
        public async Task RegisterUnableToRegisterReturnsCustomException()
        {
            // Arrange
            var userRegistrationDTO = new UserRegisterDTO
            {
                EmailId = "chai112233@gmail.com",
                PasswordClear = "chai@123"
            };
            // Act
            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User>());
            _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<UserRegisterDTO>())).ReturnsAsync((User?)null);
            // Assert
            await Assert.ThrowsExceptionAsync<CustomException>(async () =>
            {
                var result = await _userService.Register(userRegistrationDTO);
            });
        }
        #endregion

        #region TestMethod RegisterAlreadyEmailIdPresentReturnsCustomException
        [TestMethod]
        public async Task RegisterAlreadyEmailIdPresentReturnsCustomException()
        {
            // Arrange
            var userRegistrationDTO = new UserRegisterDTO
            {
                EmailId = "existinguser@gmail.com",
                PasswordClear = "newuserpassword"
            };
            var existingUser = new User { EmailId = "existinguser@gmail.com" };
            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { existingUser });

            // Act and Assert
            await Assert.ThrowsExceptionAsync<CustomException>(async () =>
            {
                var result = await _userService.Register(userRegistrationDTO);
            });
        }
        #endregion


    }
}
