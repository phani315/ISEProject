using Kanini.InvestmentSearchEngine.FinstarRating.Interfaces;
using Kanini.InvestmentSearchEngine.FinstarRating.Models.DTOs;
using Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CORS")]
    [ExcludeFromCodeCoverage]
    [Authorize]
    public class FinstarController : ControllerBase
    {
        private readonly IFinstarService _service;
        private readonly ILogger<FinstarController> _finstarLogger; 

        public FinstarController(IFinstarService service, ILogger<FinstarController> finstarLogger)
        {
            _service = service;
            _finstarLogger = finstarLogger; // Inject the logger
        }

        #region GetFinstarDetails

        [HttpPost]
        [ProducesResponseType(typeof(FinstarAverageRatingDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinstarAverageRatingDTO>> GetFinstarDetails(int id)
        {
            try
            {
                var finstar = await _service.GetFinstarDetails(id);
                return Ok(finstar);
            }
            catch (CompanyNotFound ex)
            {
                _finstarLogger.LogError(ex, "Company not found with ID: {CompanyId}", id);
                return StatusCode(400, ex.Message);
            }
            catch (SqlException ex)
            {
                _finstarLogger.LogError(ex, "SQL Exception occurred while getting Finstar details.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _finstarLogger.LogError(ex, "An unexpected error occurred while getting Finstar details.");
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region AddFinstar

        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<FinstarAverageRatingDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinstarAverageRatingDTO>> AddFinstar(FinstarDTO finstar)
        {
            try
            {
                var addedFinstar = await _service.AddFinstarDetails(finstar);
                return Ok(addedFinstar);
            }
            catch (NullReferenceException ex)
            {
                _finstarLogger.LogError(ex, "Null reference exception occurred while adding Finstar.");
                return BadRequest(ex.Message);
            }
            catch (AddObjectException ex)
            {
                _finstarLogger.LogError(ex, "Error occurred while adding Finstar.");
                return StatusCode(500, ex.Message);
            }
            catch (CompanyIdAlreadyExistsException ex)
            {
                _finstarLogger.LogError(ex, "Company ID already exists while adding Finstar.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _finstarLogger.LogError(ex, "An unexpected error occurred while adding Finstar.");
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        #endregion

        #region DeleteFinstaretails

        [HttpDelete]
        [ProducesResponseType(typeof(ActionResult<FinstarAverageRatingDTO>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinstarAverageRatingDTO>> DeleteFinstaretails(int id)
        {
            try
            {
                var result = await _service.DeleteFinstarDetails(id);
                return Ok(result);
            }
            catch (InvalidIdException ex)
            {
                _finstarLogger.LogError(ex, "Invalid Company ID while deleting Finstar.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _finstarLogger.LogError(ex, "An unexpected error occurred while deleting Finstar.");
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region UpdateFinstaretails

        [HttpPut]
        [ProducesResponseType(typeof(ActionResult<FinstarAverageRatingDTO>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinstarAverageRatingDTO>> UpdateFinstaretails(FinstarDTO finstar)
        {
            try
            {
                var result = await _service.UpdateFinstarDetails(finstar);
                return Ok(result);
            }
            catch (UpdateFailedException ex)
            {
                _finstarLogger.LogError(ex, "Update operation failed while updating Finstar.");
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                _finstarLogger.LogError(ex, "Null reference exception occurred while updating Finstar.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _finstarLogger.LogError(ex, "An unexpected error occurred while updating Finstar.");
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region GetAllFinstarDetails

        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<ICollection<FinstarAverageRatingDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<FinstarAverageRatingDTO?>?>> GetAllFinstarDetails()
        {
            try
            {
                var finstar = await _service.GetAllFinstarDetails();
                if (finstar == null)
                {
                    return NotFound("No Details are available at the moment");
                }
                return Ok(finstar);
            }
            catch (SqlException ex)
            {
                _finstarLogger.LogError(ex, "SQL Exception occurred while getting all Finstar details.");
                return StatusCode(500, ex.Message);
            }
            catch (EmptyFinstarDetails ex)
            {
                _finstarLogger.LogWarning(ex, "No Finstar details available.");
                return StatusCode(204, ex.Message);
            }
            catch (Exception ex)
            {
                _finstarLogger.LogError(ex, "An unexpected error occurred while getting all Finstar details.");
                return StatusCode(500, ex.Message);
            }
        }

        #endregion
    }
}
