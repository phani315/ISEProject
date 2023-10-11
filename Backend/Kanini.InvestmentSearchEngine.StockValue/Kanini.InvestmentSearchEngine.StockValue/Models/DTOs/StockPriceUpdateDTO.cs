using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.StockValue.Models.DTOs
{
    [ExcludeFromCodeCoverage]
    public class StockPriceUpdateDTO
    {
        #region Properties
        public int companyId { get; set; }
        public double UpdatedStockPrice { get; set; }
        public DateTime Date { get; set; }
        #endregion
    }
}
