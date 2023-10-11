using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Models
{
    
    public class Weakness
    {
        #region Properties
        [Key]
        public int WeaknessId { get; set; }
        [ForeignKey("WeaknessId")]
        [JsonIgnore]
        public SWOT? SWOT { get; set; }
        public int WeaknessValue { get; set; }
        public string? WeaknessDescription { get; set; }
        #endregion
    }
}
