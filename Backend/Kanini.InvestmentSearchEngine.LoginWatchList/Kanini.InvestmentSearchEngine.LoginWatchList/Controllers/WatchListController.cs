using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using Kanini.InvestmentSearchEngine.LoginWatchList.Error;
using System.Diagnostics.CodeAnalysis;

namespace KaniniInvestmentSearchEngine.LoginWatchList.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("ReactCors")]
    [ExcludeFromCodeCoverage]
    public class WatchListController : ControllerBase
    {
        #region Properties

        private readonly IWatchListService _watchListService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the WatchListController class.
        /// </summary>
        /// <param name="watchListService">The watchlist service for watchlist-related operations.</param>
        public WatchListController(IWatchListService watchListService)
        {
            _watchListService = watchListService;
        }

        #endregion

        #region AddingToWatchList Method

        /// <summary>
        /// Adds a company to the user's watchlist.
        /// </summary>
        /// <param name="wishlist">The watchlist item to add.</param>
        /// <returns>
        /// If the addition is successful, returns a 200 OK response with the added watchlist item.
        /// If the addition fails due to validation or other errors, returns a 400 Bad Request response with an error message.
        /// </returns>
        [ProducesResponseType(typeof(WatchList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<WatchList>> AddingToWatchList(WatchList wishlist)
        {
            try
            {
                var result = await _watchListService.AddCompanyToWatchList(wishlist);
                if (result != null)
                    return Ok(result);
            }
            catch (CustomException ue)
            {
                return BadRequest(new Error(400, ue.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest("Unable to add to wishlist");
        }

        #endregion

        #region GetAllWatchList Method

        /// <summary>
        /// Retrieves the user's entire watchlist.
        /// </summary>
        /// <param name="watchListUserIdDTO">The user ID for whom to retrieve the watchlist.</param>
        /// <returns>
        /// If the retrieval is successful, returns a 200 OK response with the user's watchlist.
        /// If the retrieval fails due to validation or other errors, returns a 400 Bad Request response with an error message.
        /// </returns>
        [ProducesResponseType(typeof(ActionResult<WatchListsCompanysDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<WatchListsCompanysDTO>> GetAllWatchList(WatchListUserIdDTO watchListUserIdDTO)
        {
            try
            {
                var results = await _watchListService.ViewWatchList(watchListUserIdDTO);
                return Ok(results);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region RemovingFromWatchList Method

        /// <summary>
        /// Removes a company from the user's watchlist.
        /// </summary>
        /// <param name="getWishlistIdDTO">The watchlist item to remove.</param>
        /// <returns>
        /// If the removal is successful, returns a 200 OK response with the removed watchlist item.
        /// If the removal fails due to validation or other errors, returns a 400 Bad Request response with an error message.
        /// If no matching data is found for removal, returns a 400 Bad Request response with an error message.
        /// </returns>
        [ProducesResponseType(typeof(ActionResult<WatchList>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<ActionResult<WatchList>> RemovingFromWatchList(GetWatchListIdDTO getWishlistIdDTO)
        {
            try
            {
                var results = await _watchListService.RemovingFromWatchList(getWishlistIdDTO);
                if (results != null)
                    return Ok(results);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("No data found");
        }

        #endregion
    }
}
