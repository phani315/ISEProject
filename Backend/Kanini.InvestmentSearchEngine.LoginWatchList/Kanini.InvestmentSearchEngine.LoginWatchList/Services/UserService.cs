using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using System.Security.Cryptography;
using System.Text;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Services
{
    /// <summary>
    /// This class provides services related to user authentication and registration.
    /// It implements the <see cref="IUserService"/> interface.
    /// </summary>
    public class UserService : IUserService
    {
        #region Properties
        private readonly IRepository<int, User> _userRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for data access.</param>
        /// <param name="mapper">The mapper for mapping user data.</param>
        public UserService(IRepository<int, User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #endregion

        #region Login Method

        /// <summary>
        /// Logs in a user with the provided credentials and returns a user login DTO if successful.
        /// </summary>
        /// <param name="userDTO">The user login DTO containing login credentials.</param>
        /// <returns>The user login DTO if authentication is successful; otherwise, throws exceptions.</returns>
        public async Task<UserLoginDTO?> Login(UserLoginDTO userDTO)
        {
            var userInfo = await _userRepository.GetAll();
            var userdata = userInfo?.FirstOrDefault(u => u.EmailId == userDTO.EmailId);

            if (userdata == null || userdata.PasswordKey == null || userDTO.Password == null)
                throw new CustomException("Please register, you are not registered");

            using var hmac = new HMACSHA512(userdata.PasswordKey);
            var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            if (userPass == null || userdata.PasswordHash == null ||
                userPass.Length != userdata.PasswordHash.Length ||
                !userPass.SequenceEqual(userdata.PasswordHash))
            {
                throw new CustomException("Invalid username or password");
            }
            return _mapper.UserToUserLoginDTO(userdata);
        }

        #endregion

        #region Register Method

        /// <summary>
        /// Registers a new user with the provided registration data and returns a user login DTO if successful.
        /// </summary>
        /// <param name="userRegistrationDTO">The user registration DTO containing registration data.</param>
        /// <returns>The user login DTO if registration is successful; otherwise, throws exceptions.</returns>
        public async Task<UserLoginDTO?> Register(UserRegisterDTO userRegistrationDTO)
        {
            UserLoginDTO? userLoginDTO = null;
            var hmac = new HMACSHA512();
            if (userRegistrationDTO.PasswordClear != null)
            {
                userRegistrationDTO.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegistrationDTO.PasswordClear));
                userRegistrationDTO.PasswordKey = hmac.Key;
            }
            var users = await _userRepository.GetAll();
            if (users != null)
            {
                var validUser = users.FirstOrDefault(u => u.EmailId == userRegistrationDTO.EmailId);
                if (validUser != null)
                {
                    throw new CustomException("Email address already in use");
                }
            }
            var user = await _userRepository.Add(userRegistrationDTO);
            if (user != null)
                userLoginDTO = _mapper.UserToUserLoginDTO(user);
            return userLoginDTO ?? throw new CustomException("Unable to register");
        }

        #endregion
    }
}
