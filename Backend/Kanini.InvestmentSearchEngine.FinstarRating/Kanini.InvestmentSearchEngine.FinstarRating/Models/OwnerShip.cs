using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Models
{
    [ExcludeFromCodeCoverage]

    public class OwnerShip
    {
        #region Properties
        [Key]
        public int OwnerShipID { get; set; }
        [ForeignKey("OwnerShipID")]
        [JsonIgnore]
        public Finstar? Finstar { get; set; }
        [Range(0, 5, ErrorMessage = "TotalRating must be between 0 and 5.")]
        public double OwnerShipRate { get; set; }
        [Required(ErrorMessage = "ReviewCount is required.")]
        [Range(0, 10000, ErrorMessage = "ReviewCount must be between 0 and 5000.")]
        public int ReviewCount { get; set; }
        #endregion

    }

}
