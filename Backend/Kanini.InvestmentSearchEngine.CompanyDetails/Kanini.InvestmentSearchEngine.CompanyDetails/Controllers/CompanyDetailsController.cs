using Microsoft.AspNetCore.Mvc;
using Kanini.InvestmentSearchEngine.CompanyDetails.Models;
using Kanini.InvestmentSearchEngine.CompanyDetails.Interfaces;
using Kanini.InvestmentSearchEngine.CompanyDetails.Exceptions;
using Microsoft.AspNetCore.Cors;


namespace Kanini.InvestmentSearchEngine.CompanyDetails.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CORS")]
    public class CompanyDetailsController : ControllerBase
    {
        private readonly ICompanyDetailsServices _companyDetailsServices;

        public CompanyDetailsController(ICompanyDetailsServices companyDetailsServices)
        {
            _companyDetailsServices = companyDetailsServices;
        }

        #region GetAllCompanyDetails
        [HttpGet]
        [ProducesResponseType(typeof(List<CompanyDetail>),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<CompanyDetail>>> GetAllCompanyDetails()
        {
            try
            {
                var companyDetails = await _companyDetailsServices.GetAllCompanyDetails();
                return Ok(companyDetails);
            }
            catch(DataNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        #endregion

        #region GetCompanyDetails
        [HttpPost("{companyId}")]
        [ProducesResponseType(typeof(CompanyDetail),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CompanyDetail>> GetCompanyDetailsById(int companyId)
        {
            try
            {
                var companyDetails = await _companyDetailsServices.GetCompanyDetailsById(companyId);
                if (companyDetails == null)
                    return NotFound();

                return Ok(companyDetails);
            }
            catch (DataNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        #endregion

        #region AddCompanyDetails
        [HttpPost]
        [ProducesResponseType(typeof(CompanyDetail), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCompanyDetails(CompanyDetail companyDetails)
        {
            try
            {
                var result = await _companyDetailsServices.AddCompanyDetails(companyDetails);
                if (result != null)
                {
                    return Created("CompanyDetails", result);
                }
                return BadRequest("Not added");
            }
            catch (DataNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            } 
        }
        #endregion

        #region UpdatDetails

        [HttpPut]
        [ProducesResponseType(typeof(CompanyDetail), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCompanyDetails(CompanyDetail companyDetails)
        {
            try
            {
                var updatedCompanyDetails = await _companyDetailsServices.UpdateCompanyDetails(companyDetails);
                return Ok(updatedCompanyDetails);
            }
            catch (DataNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }

        }
        #endregion

        #region DeleteDetail

        [HttpDelete("{companyId}")]
        [ProducesResponseType(typeof(CompanyDetail), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCompanyDetails(int companyId)
        {
            try
            {
                var deletedCompanyDetails = await _companyDetailsServices.DeleteCompanyDetails(companyId);
                if (deletedCompanyDetails == null)
                    return NotFound();

                return Ok(deletedCompanyDetails);
            }
            catch (DataNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        #endregion

        [HttpPost]
        [ProducesResponseType(typeof(List<CompanyDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchCompanies(string searchTerm)
        {
            try
            {
                var results = await _companyDetailsServices.SearchCompanies(searchTerm);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
