using System.ComponentModel.DataAnnotations;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Models
{
    public class SWOT
    {
        #region Properties
        [Key]
        public int SwotId { get; set; }
        public int CompanyID { get; set; }
        public Strength? Strength { get; set; }
        public Weakness? Weakness { get; set; }
        public Threat? Threat { get; set; }
        public Oppurtunity? Oppurtunity { get; set; }
        public DateTime Date { get; set; }
        #endregion
    }
}
