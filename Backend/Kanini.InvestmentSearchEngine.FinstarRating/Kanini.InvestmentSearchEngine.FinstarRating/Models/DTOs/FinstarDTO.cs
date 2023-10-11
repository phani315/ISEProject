using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Models.DTOs
{
    [ExcludeFromCodeCoverage]

    public class FinstarDTO
    {
        public int CompanyId { get; set; }
        public double EfficiencyRate { get; set; }
        public int EfficienncyReviewCount { get; set; }
        public double FinancialRate { get; set; }
        public int FinancialReviewCount { get; set; }
        public double OwnerShipRate { get; set; }
        public int OwnerShipReviewCount { get; set; }
        public double ValuationRate { get; set; }
        public int ValuationReviewCount { get; set; }
    }
}
