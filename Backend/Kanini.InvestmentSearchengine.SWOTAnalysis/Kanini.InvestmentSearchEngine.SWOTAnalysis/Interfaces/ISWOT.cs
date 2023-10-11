using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.DTOs;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces
{
    public interface ISWOT
    {
        #region SWOTValuesAndDescription Repository Interface
        Task<SwotDTO?> GetSWOTDetailsByCompanyID(int companyID);
        #endregion
    }
}
