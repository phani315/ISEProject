using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.CompanyEssentials.CustomExceptions
{
    [ExcludeFromCodeCoverage]
    public class GroupExceptions : Exception
    {
        #region Custom Exception
        public GroupExceptions() { }
        public GroupExceptions(string messages) : base(messages) { }
        #endregion
    }
}
