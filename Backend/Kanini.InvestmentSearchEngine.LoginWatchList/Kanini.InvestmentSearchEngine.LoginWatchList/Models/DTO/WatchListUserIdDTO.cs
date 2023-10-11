
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO
{

    [ExcludeFromCodeCoverage]
    public class WatchListUserIdDTO
    {
        #region Properties
        [Required]
        public int UserId { get; set; }
        #endregion

    }
}
