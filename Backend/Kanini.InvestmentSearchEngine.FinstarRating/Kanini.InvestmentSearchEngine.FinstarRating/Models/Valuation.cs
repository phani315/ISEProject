using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Models
{
    [ExcludeFromCodeCoverage]

    public class Valuation
    {
        #region Properties
            [Key]
            public int ValuationID { get; set; }
            [ForeignKey("ValuationID")]
            [JsonIgnore]
            public Finstar? Finstar { get; set; }
            [Range(0, 5, ErrorMessage = "TotalRating must be between 0 and 5.")]

            public double ValuationRate { get; set; }
            [Required(ErrorMessage = "ReviewCount is required.")]
            [Range(0, 10000, ErrorMessage = "ReviewCount must be between 0 and 5000.")]

            public int ReviewCount { get; set; }
        #endregion

    }
}
