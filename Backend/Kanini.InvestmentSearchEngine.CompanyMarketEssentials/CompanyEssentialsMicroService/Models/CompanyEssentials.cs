using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KaniniInvestmentSearchEngineCompanyMarketEssentials.Models
{
    [ExcludeFromCodeCoverage]
    public class CompanyEssentials
    {
        #region Properties
        [Key] 
        public int EssenID { get; set; }
        public int CompanyID { get; set; } 

        [Required(ErrorMessage ="MarketCap is required")] 
        public double MarketCap { get; set; }

        [Required(ErrorMessage ="Enterprive value is required")]
        public double EnterpriceValue { get; set; }

        [Required(ErrorMessage ="No of Shares can not be null")]
        public double NoOfShares { get; set; }

        [Required(ErrorMessage ="Div Yield is required")]
        public double DivYield { get; set; }

        [Required(ErrorMessage ="Cash is required")]
        public double Cash { get; set; }

        [Required(ErrorMessage ="Promoter holding should not null")]
        public double PromoterHolding { get; set; }

        [Required(ErrorMessage ="Price is Required")] 
        public double Price { get; set; }
        public double BookValue { get; set; }

        [Range(0,50)]
        [Required] 
        public double? PriceToBook { get; set; }

        [Range(0,100)]
        [Required]
        public double? PriceToEarning { get; set; }

        [Range(0,99)]
        [Required]
        public double? Eps { get; set; }

        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "NetIncome should be a valid number.")]
        public double NetIncome { get; set; }

        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Sector should contain only alphabets.")]
        public string? Sector { get; set; }
        #endregion
    }
}
