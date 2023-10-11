using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Models
{
    public class Threat
    {
        #region Properties


        [Key]
        public int ThreatId { get; set; }
        [ForeignKey("ThreatId")]
        [JsonIgnore]
        public SWOT? SWOT { get; set; }
        public int ThreatValue { get; set; }
        public string? ThreatDescription { get; set; }
        #endregion
    }
}
