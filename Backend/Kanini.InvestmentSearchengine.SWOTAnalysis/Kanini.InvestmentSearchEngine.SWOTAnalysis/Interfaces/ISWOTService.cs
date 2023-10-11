using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.DTOs;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces
{
    public interface ISWOTService
    {
        #region SWOTService Interface
        public Task<SWOT?> AddSwotDetails(SWOT swot);
        public Task<SWOT?> UpdateSwotDetails(SWOT swot);
        public Task<SWOT?> DeleteSwotDeatils(int swotId);
        Task<SwotDTO?> GetSWOTDetailsByCompanyID(int companyID);
        public Task<ICollection<SWOT>?> GetAllSwotDetails();
        #endregion
    }
}
