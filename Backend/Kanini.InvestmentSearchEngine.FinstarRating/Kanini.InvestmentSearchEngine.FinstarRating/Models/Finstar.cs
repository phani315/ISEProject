using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Models
{
    [ExcludeFromCodeCoverage]
    public class Finstar
    {
        #region Properties
        [Key]
        public int RatingId { get; set; }
        public int CompanyId { get; set; }
        [Range(0, 5, ErrorMessage = "TotalRating must be between 0 and 5.")]
        public double TotalRating { get; set; }
        [Required(ErrorMessage = "TotalReviewCount is required.")]
        [Range(0, 10000, ErrorMessage = "TotalReviewCount must be between 0 and 5000.")]
        public int TotalReviewCount { get; set; }
        public Financial? Financial { get; set; }
        public OwnerShip? OwnerShip { get; set; }
        public Efficiency? Efficiency { get; set; }
        public Valuation? Valuation { get; set; }
        #endregion
    }
}
