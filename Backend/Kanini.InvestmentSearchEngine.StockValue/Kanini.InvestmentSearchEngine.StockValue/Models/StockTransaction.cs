using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Kanini.InvestmentSearchEngine.StockValue.Models
{
    [ExcludeFromCodeCoverage]
    public class StockTransaction
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public int StockId { get; set; }
        [ForeignKey("StockId")]
        [JsonIgnore]
        public StockPrice? StockPrice { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "StockValue must be a valid numeric value with up to 2 decimal places.")]
        public double StockValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        #endregion
    }
}
