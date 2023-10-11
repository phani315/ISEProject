using Microsoft.AspNetCore.Mvc;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.DTOs;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces;
using Microsoft.AspNetCore.Cors;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Exceptions;
using Microsoft.Data.SqlClient;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CORS")]
    public class SWOTController : ControllerBase
    {
        #region Private Field
        private readonly ISWOTService _swotService;
        #endregion

        #region Constructor
        public SWOTController(ISWOTService swotService)
        {
            _swotService = swotService;
        }
        #endregion

        #region Get All SWOT Details
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<ICollection<SWOT>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<SWOT>>> GetAllSWOTDetails()
        {
            try
            {
                var swot = await _swotService.GetAllSwotDetails();
                if (swot == null)
                {
                    return NotFound("No Details are available at the moment");

                }
                return Ok(swot);
            }
            catch (NullReferenceException exe)
            {
                return BadRequest(exe.Message);
            }
        }
        #endregion

        #region Get SWOT Details By Company ID
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<SwotDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SwotDTO>> GetSwotByCompanyID(int companyId)
        {
            try
            {
                var swot = await _swotService.GetSWOTDetailsByCompanyID(companyId);
                if (swot == null)
                {
                    return NotFound("No Details are available at the moment");
                }
                return Ok(swot);
            }
            catch (NullCompanyDetailsException ncde)
            {
                return BadRequest(ncde.Message);
            }
            catch (SqlException)
            {
              return BadRequest("network error please try again");
            }
        }
        #endregion

        #region Add SWOT Details
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<SWOT>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<SWOT>>> AddSwotDetails(SWOT swot)
        {
            try
            {
                var result = await _swotService.AddSwotDetails(swot);
                if (result == null)
                {
                    return NotFound("unable to add the swot details");
                }
                return Created("Home", result);
            }
            catch (NullSWOTDetailsException ncde)
            {
                return BadRequest(ncde.Message);
            }
        }
        #endregion

        #region Update SWOT Details
        [HttpPut]
        [ProducesResponseType(typeof(ActionResult<SWOT>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SWOT>> UpdateSWOTDetails(SWOT swot)
        {
            try
            {
                var result = await _swotService.UpdateSwotDetails(swot);
                if (result == null)
                {
                    return BadRequest("Unable to update swot details");

                }
                return Ok(result);
            }
            catch (NullSWOTDetailsException ncde)
            {
                return BadRequest(ncde.Message);
            }
        }
        #endregion

        #region Delete SWOT Details
        [HttpDelete]
        [ProducesResponseType(typeof(ActionResult<SWOT>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SWOT>> DeleteSWOTDetails(int id)
        {
            try
            {
                var result = await _swotService.DeleteSwotDeatils(id);
                if (result == null)
                {
                    return BadRequest("Unable to Delete swot details");

                }
                return Ok(result);
            }
            catch (NullSWOTDetailsException ncde)
            {
                return BadRequest(ncde.Message);
            }
        }
        #endregion
    }
}

