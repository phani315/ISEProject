using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class GetWatchListIdDTO
    {
        #region Properties

        public int UserId { get; set; }
        public int CompanyId { get; set; }

        #endregion
    }
}
