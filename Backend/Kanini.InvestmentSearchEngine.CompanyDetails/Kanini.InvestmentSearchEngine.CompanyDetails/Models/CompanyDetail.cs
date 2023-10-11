using System.ComponentModel.DataAnnotations;

namespace Kanini.InvestmentSearchEngine.CompanyDetails.Models
{
    public class CompanyDetail
    {
        [Key] 
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; } 

        [Required(ErrorMessage ="NSE in required")]
        public string? NSE { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "BSE must contain only numeric characters.")]
        public string?  BSE { get; set; }

        [Required(ErrorMessage ="Sector should not be null")]
        public string? Sector { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public string? Image { get; set; }
    }
}
