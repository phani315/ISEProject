using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
namespace Kanini.InvestmentSearchEngine.LoginWatchList.Models
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com$", ErrorMessage = "Invalid email address")]
        public string? EmailId { get; set; }

        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }

        [JsonIgnore]
        public byte[]? PasswordKey { get; set; }

        public UserInfo? UserInfo { get; set; }

        #endregion
    }
}
