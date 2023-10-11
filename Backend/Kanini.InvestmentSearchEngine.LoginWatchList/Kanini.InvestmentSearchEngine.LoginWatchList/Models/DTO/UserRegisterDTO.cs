using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class UserRegisterDTO : User
    {
        #region Properties
        public string ?PasswordClear { get; set; }
        #endregion

    }
}
