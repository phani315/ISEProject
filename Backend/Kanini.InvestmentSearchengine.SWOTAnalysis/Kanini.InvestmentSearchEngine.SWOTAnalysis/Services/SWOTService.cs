using Kanini.InvestmentSearchEngine.SWOTAnalysis.Exceptions;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.DTOs;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Repositories;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Services
{
    public class SWOTService : ISWOTService
    {
        #region Private Fields
        private readonly IRepository<int, SWOT> _swotRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the SWOTService class.
        /// </summary>
        /// <param name="swotRepository">The repository used for SWOT analysis operations.</param>
        public SWOTService(IRepository<int, SWOT> swotRepository ,IMapper mapper)
        {
            _swotRepository = swotRepository;
            _mapper = mapper;
            
        }
        #endregion


        #region Services Method To Add SWOT Details
        /// <summary>
        /// Add SWOT details to the repository.
        /// </summary>
        /// <param name="swot">The SWOT object to be added.</param>
        /// <returns>The added SWOT object.</returns>
        public async Task<SWOT?> AddSwotDetails(SWOT swot)
        {
            return await _swotRepository.Add(swot);
        }
        #endregion

        #region Services Method To Update SWOT Details
        /// <summary>
        /// Update existing SWOT details in the repository.
        /// </summary>
        /// <param name="swot">The updated SWOT object.</param>
        /// <returns>The updated SWOT object if successful, otherwise throws an exception.</returns>
        /// <exception cref="NullSWOTDetailsException">Thrown when the update is not successful.</exception>
        public async Task<SWOT?> UpdateSwotDetails(SWOT swot)
        {
            return await _swotRepository.Update(swot) ?? throw new NullSWOTDetailsException("not updated");
        }
        #endregion

        #region Services Method To Delete SWOT Details
        /// <summary>
        /// Delete SWOT details from the repository.
        /// </summary>
        /// <param name="swotId">The ID of the SWOT details to be deleted.</param>
        /// <returns>The deleted SWOT object if successful, otherwise throws an exception.</returns>
        /// <exception cref="NullSWOTDetailsException">Thrown when the deletion is not successful.</exception>
        public async Task<SWOT?> DeleteSwotDeatils(int swotId)
        {
            return await _swotRepository.Delete(swotId) ?? throw new NullSWOTDetailsException("not deleted");
        }
        #endregion

        #region Get SWOT Details By Company ID
        /// <summary>
        /// Get SWOT details for a company based on its ID.
        /// </summary>
        /// <param name="companyID">The ID of the company to retrieve SWOT details for.</param>
        /// <returns>A SwotDTO containing SWOT details if available, otherwise throws an exception.</returns>
        /// <exception cref="NullCompanyDetailsException">Thrown when no SWOT details are available for the given company ID.</exception>
        public async Task<SwotDTO?> GetSWOTDetailsByCompanyID(int companyID)
        {

            var swotInformation = await _swotRepository.GetAll()??throw new Exception("no data found");
            var swotDetails = swotInformation.ToList().LastOrDefault(s => s.CompanyID == companyID) ?? throw new NullCompanyDetailsException("No swot details available with given company Id");
            return _mapper.SwotToSwotDTO(swotDetails);
        }
        #endregion
        #region GetAllSwotDetails()
        public async Task<ICollection<SWOT>?> GetAllSwotDetails()
        {
            return await _swotRepository.GetAll() ?? throw new NullSWOTDetailsException("not found");

        }
        #endregion
    }



}


