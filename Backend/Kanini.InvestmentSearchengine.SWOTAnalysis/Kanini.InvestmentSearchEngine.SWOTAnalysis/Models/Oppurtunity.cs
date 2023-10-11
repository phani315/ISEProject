using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Models
{
    public class Oppurtunity
    {
        #region Properties
        [Key]
        public int OppurtunityId { get; set; }
        [ForeignKey("OppurtunityId")]
        [JsonIgnore]
        public SWOT? SWOT { get; set; }
        public int OppurtunityValue { get; set; }
        public string? OppurtunityDescription { get; set; }
        #endregion

    }
}
