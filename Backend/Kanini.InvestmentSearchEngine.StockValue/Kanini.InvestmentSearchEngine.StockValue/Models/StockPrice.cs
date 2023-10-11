using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.StockValue.Models
{
    [ExcludeFromCodeCoverage]
    public class StockPrice:IEquatable<StockPrice>
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CompanyId must be a positive integer.")]
        public int CompanyId { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "CurrentStockPrice must be a valid numeric value with up to 2 decimal places.")]
        public double CurrentStockPrice { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "UpdatedStockPrice must be a valid numeric value with up to 2 decimal places.")]
        public double UpdatedStockPrice { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "UpdatedStockPercent must be a valid numeric value with up to 2 decimal places.")]
        public double UpdatedStockPercent { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public ICollection<StockTransaction>? StockTransactions { get; set; }
        #endregion

        public bool Equals(StockPrice? other)
        {
            if (other == null)
                return false;
            return this.CompanyId == other.CompanyId;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return (Equals((StockPrice)obj));
        }

        public override int GetHashCode()
        {
            return CompanyId.GetHashCode();
        }
    }
}
