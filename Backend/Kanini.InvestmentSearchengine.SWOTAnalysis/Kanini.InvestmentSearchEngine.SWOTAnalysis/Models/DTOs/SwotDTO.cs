namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.DTOs
{
    public class SwotDTO
    {
        #region Properties

        public int CompanyID { get; set; }
        public int StrengthValue { get; set; }
        public string? StrengthDescription { get; set; }
        public int ThreatValue { get; set; }
        public string? ThreatDescription { get; set; }
        public int WeaknessValue { get; set; }
        public string? WeaknessDescription { get; set; }
        public int OppurtunityValue { get; set; }
        public string? OppurtunityDescription { get; set; }
        #endregion
    }
}
