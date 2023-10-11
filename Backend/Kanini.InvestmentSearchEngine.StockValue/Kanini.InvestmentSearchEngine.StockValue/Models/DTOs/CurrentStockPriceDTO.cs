using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.StockValue.Models.DTOs
{
    [ExcludeFromCodeCoverage]

    public class CurrentStockPriceDTO
    {
        #region Properties
        public double CurrentStockPrice { get; set; }
        public double UpdatedStockPrice { get; set; }
        public double UpdatedStockPercent { get; set; }
        public DateTime Date { get; set; }
        #endregion
    }
}
