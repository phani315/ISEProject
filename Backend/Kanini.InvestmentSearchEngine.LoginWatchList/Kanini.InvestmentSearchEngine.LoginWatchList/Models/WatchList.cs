using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Models
{
    [ExcludeFromCodeCoverage]
    public class WatchList
    {
        #region Properties

        [Key]
        public int WatchListId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public UserInfo? UserInfo { get; set; }

        [Required]
        public int CompanyId { get; set; }

        #endregion
    }
}
