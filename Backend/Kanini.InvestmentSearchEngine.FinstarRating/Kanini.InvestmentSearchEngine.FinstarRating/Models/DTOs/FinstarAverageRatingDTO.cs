using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Models.DTOs
{
    [ExcludeFromCodeCoverage]

    public class FinstarAverageRatingDTO:FinstarDTO
    {
        public double TotalRating { get; set; }
        public int TotalReviewCount { get; set; }   
    }
}
