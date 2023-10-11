using Kanini.InvestmentSearchEngine.CompanyEssentials.CustomExceptions;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Interfaces;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Models;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace KaniniInvestmentSearchEngineCompanyMarketEssentials.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController] 
    [EnableCors("CORS")]
    [ExcludeFromCodeCoverage]
    public class CompanyEssentialsController : ControllerBase
    {
        #region Private Fields
        private readonly ICompanyEssentialsServices<int, CompanyEssentials> _companyEssentialsService;
        private readonly ILogger<CompanyEssentialsServices> _companyEssentialsLogger;
        #endregion

        #region Constructor
        public CompanyEssentialsController(ICompanyEssentialsServices<int, CompanyEssentials> companyEssService, ILogger<CompanyEssentialsServices> logger)
        {
            _companyEssentialsService = companyEssService;
            _companyEssentialsLogger = logger;
        } 
        #endregion

        #region AddEssentials
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<CompanyEssentials>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult?> AddEssentials(CompanyEssentials company)
        {
            try
            {
                var addedCompany = await _companyEssentialsService.AddEssentials(company);

                if (addedCompany == null)  return BadRequest(new { message = "Company already exists" });
                 return Ok(addedCompany);
            }
            catch (NullReferenceException ex) 
            {
                return BadRequest($"NullReferenceException: {ex.Message}");
            }
            catch (GroupExceptions ex)
            {
                return BadRequest($"GroupExceptions: {ex.Message}");
            }
            catch (Exception ex)
            {
                _companyEssentialsLogger.LogError(ex, "Error occurred while adding a Company");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
        #endregion

        #region GetAllEssentials
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<CompanyEssentials>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult?> GetAllEssentials()
        {
            try
            {
                var essentials = await _companyEssentialsService.GetAllEssentials();

                if (essentials == null)
                {
                    return BadRequest(new { message = "No records are available" });
                }

                return Ok(essentials);
            }
            catch (Exception ex)
            {
                _companyEssentialsLogger.LogError(ex, "Error occurred while getting all the Company Essentials");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        #endregion

        #region UpdateEssentials
        [HttpPut]
        [ProducesResponseType(typeof(CompanyEssentials), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult?> UpdateEssential([FromBody] CompanyEssentials Ess)
        {
            try
            {
                var result = await _companyEssentialsService.UpdateEssential(Ess);

                if (result == null)
                {
                    return BadRequest(new { message = "Essential is not available" });
                }

                return Ok(result);
            }
            catch (GroupExceptions ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _companyEssentialsLogger.LogError(ex, "Error occurred while updating a Company");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        #endregion

        #region DeleteEssentials
        [HttpDelete]
        [ProducesResponseType(typeof(CompanyEssentials), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult?> DeleteEssentials(int id)
        {
            try
            {
                var result = await _companyEssentialsService.DeleteEssential(id);
                if (result == null)
                {
                    return BadRequest(new { message = "Essential is not available" });
                }
                return Ok(result);
            }
            catch (GroupExceptions ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _companyEssentialsLogger.LogError(ex, "Error occurred while deleting a Company");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        #endregion

        #region GetEssentialsByCompany
        [HttpPost]
        [ProducesResponseType(typeof(CompanyEssentials), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult?> GetEssentialsByCompany(int id)
        {
            try
            {
                var result = await _companyEssentialsService.GetEssential(id);

                if (result == null)
                {
                    return BadRequest(new { message = "Company not available" });
                }

                return Ok(result);
            }
            catch (GroupExceptions ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _companyEssentialsLogger.LogError(ex, "Error occurred while getting a Company");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        #endregion

        #region GetFilteredCompanies
        [HttpGet("filtered-companies")]
        [ProducesResponseType(typeof(ICollection<CompanyEssentials?>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult?> GetFilteredCompanies()
        {
            try
            {
                var filteredCompanies = await _companyEssentialsService.FilterCompanies();

                if (filteredCompanies == null)
                {
                    return BadRequest(new { message = "Failed to retrieve companies." });
                }
                else if (!filteredCompanies.Any())
                {
                    return NoContent();
                }

                return Ok(filteredCompanies);
            }
            catch (GroupExceptions ex)
            {
                return BadRequest(new { message = ex.Message });
            } 
            catch (Exception ex)
            {
                _companyEssentialsLogger.LogError(ex, "Error occurred while getting the filtered Companies");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        #endregion
    }
}
