using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class UserController : ControllerBase
    {
        #region Properties

        private readonly IUserService _userService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="userService">The user service for user-related operations.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Registration Method

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userDTO">The user registration DTO.</param>
        /// <returns>
        /// If registration is successful, returns a 201 Created response with the registered user's information.
        /// If registration fails due to validation or other errors, returns a 400 Bad Request response with an error message.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserLoginDTO), StatusCodes.Status201Created)] // Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Failure Response
        public async Task<ActionResult<UserLoginDTO>> Registration(UserRegisterDTO userDTO)
        {
            try
            {
                var user = await _userService.Register(userDTO);
                return Created("Registration done", user);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Network error...Please try again");
            }
        }

        #endregion

        #region Login Method

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userLoginDTO">The user login DTO.</param>
        /// <returns>
        /// If login is successful, returns a 200 OK response with the user's login information.
        /// If login fails due to validation or other errors, returns a 400 Bad Request response with an error message.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserLoginDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLoginDTO>> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userService.Login(userLoginDTO);
                if (user != null)
                {
                    return Ok(user);
                }
                return BadRequest("Unable to Login");
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Network error...Please try again");
            }
        }

        #endregion
    }
}
