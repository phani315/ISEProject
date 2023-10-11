using Moq;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Kanini.InvestmentSearchEngine.LoginWatchList.Services;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using System.Text;
using System.Security.Cryptography;

namespace TestLoginWatchList.TestLogin
{
    [TestClass]
    public class TestLogin
    {
        #region Properties
        private Mock<IRepository<int, User>> _userRepositoryMock=null!;
        private Mock<IMapper> _mapperMock=null!;
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

        #region TestMethod LoginValidUserReturnsUserLoginDTO

        [TestMethod]
        public async Task LoginValidUserReturnsUserLoginDTO()
        {
            // Arrange
            var email = "shivkkumar112233@gmail.com";
            var password = "Siva@1234";
            var passwordKey = Encoding.UTF8.GetBytes("yourPasswordKey"); 
            var passwordHash = new HMACSHA512(passwordKey).ComputeHash(Encoding.UTF8.GetBytes(password));

            var userDTO = new UserLoginDTO
            {
                EmailId = email,
                Password = password
            };

            var userInDatabase = new User
            {
                EmailId = email,
                PasswordKey = passwordKey,
                PasswordHash = passwordHash
            };

            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { userInDatabase });

            // Mock the IMapper behavior
            var expectedUserLoginDTO = new UserLoginDTO
            {
                UserId = 1,
                Token = "This is my token key",
                EmailId = email
            };
            _mapperMock.Setup(mapper => mapper.UserToUserLoginDTO(userInDatabase)).Returns(expectedUserLoginDTO);

            // Act
            var result = await _userService.Login(userDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUserLoginDTO.UserId, result.UserId);
            Assert.AreEqual(expectedUserLoginDTO.Token, result.Token);
        }
        #endregion

        #region TestMethod LoginUserNotFoundThrowsCustomException
        [TestMethod]
        public async Task LoginUserNotFoundThrowsCustomException()
        {
            // Arrange
            var userDTO = new UserLoginDTO
            {
                EmailId = "raju1122@gmail.com",
                Password = "Raju@123"
            };

            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User>());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _userService.Login(userDTO));
        }
        #endregion

        #region TestMethod LoginNullPasswordThrowsCustomException
        [TestMethod]
        public async Task LoginNullPasswordThrowsCustomException()
        {
            // Arrange
            var userDTO = new UserLoginDTO
            {
                EmailId = "sam112233@gmail.com",
                Password = null
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _userService.Login(userDTO));
        }
        #endregion

        #region TestMethod LoginInvalidPasswordThrowsCustomException
        [TestMethod]
        public async Task LoginInvalidPasswordThrowsCustomException()
        {
            // Arrange
            var email = "testuser@gmail.com";
            var validPassword = "ValidPassword123";
            var invalidPassword = "InvalidPassword456";
            var passwordKey = Encoding.UTF8.GetBytes("yourPasswordKey");
            var validPasswordHash = new HMACSHA512(passwordKey).ComputeHash(Encoding.UTF8.GetBytes(validPassword));

            var userDTO = new UserLoginDTO
            {
                EmailId = email,
                Password = invalidPassword
            };

            var userInDatabase = new User
            {
                EmailId = email,
                PasswordKey = passwordKey,
                PasswordHash = validPasswordHash
            };

            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { userInDatabase });

            // Act and Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _userService.Login(userDTO));
        }
        #endregion


    }
}
