using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.StockValue.Models.DTOs
{
    [ExcludeFromCodeCoverage]
    public class StockPriceAveragesDTO
    {
        #region Properties
        public int CompanyID { get; set; }
        public double TodayHigh { get; set; }
        public double TodayLow { get; set; }
        public double YearHigh { get; set; }
        public double YearLow { get; set; }
        public DateTime Date { get; set; }
        #endregion
    }
}
