using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Mapper
{
    [ExcludeFromCodeCoverage]
    public class UserToUserDTO : IMapper
    {
        #region Properties

        private readonly ITokenGenerate _tokenService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UserToUserDTO class.
        /// </summary>
        /// <param name="tokenGenerate">The token generation service.</param>
        public UserToUserDTO(ITokenGenerate tokenGenerate)
        {
            _tokenService = tokenGenerate;
        }

        #endregion

        #region UserToUserLoginDTO Method

        /// <summary>
        /// Maps a User object to a UserLoginDTO object and generates a token for authentication.
        /// </summary>
        /// <param name="user">The User object to map.</param>
        /// <returns>A UserLoginDTO object with user details and a generated authentication token.</returns>
        public UserLoginDTO? UserToUserLoginDTO(User user)
        {
            var userLoginDTO = new UserLoginDTO
            {
                EmailId = user.EmailId,
                UserId = user.Id
            };
            userLoginDTO.Token = _tokenService.GenerateToken(userLoginDTO);

            return userLoginDTO;
        }

        #endregion
    }
}
