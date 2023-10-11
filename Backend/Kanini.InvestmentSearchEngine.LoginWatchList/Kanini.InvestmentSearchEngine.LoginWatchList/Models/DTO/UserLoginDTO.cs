using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class UserLoginDTO
    {
        #region Properties
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com$", ErrorMessage = "Invalid email address")]
        public string? EmailId { get; set; }
        public string? Password { get; set; }

        public int  UserId { get; set; }

        public string? Token { get; set; }

        #endregion


    }
}
