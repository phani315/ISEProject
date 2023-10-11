using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Models
{
    public class Strength
    {
        #region Properties
        [Key]
        public int StrengthId { get; set; }
        [ForeignKey("StrengthId")]
        [JsonIgnore]
        public SWOT? SWOT { get; set; }
        public int StrengthValue { get; set; }
        public string? StrengthDescription { get; set; }
        #endregion
    }
}
