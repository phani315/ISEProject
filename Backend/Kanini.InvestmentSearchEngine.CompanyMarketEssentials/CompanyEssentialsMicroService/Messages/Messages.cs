using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.CompanyMarketEssentials.Messages
{
    [ExcludeFromCodeCoverage]
    public class Messages
    {
        #region Private Field
        public static readonly List<string> messages = new();
        #endregion

        #region Custom Exceptions Messages
        static Messages()
        {
            messages = new List<string>()
            {
                "The Company Essentials context is null",//1
                "Failed to delete the company essentials",//2
                "Failed to retrieve companies",//3
                "Failed to add Company Essentials",//4
                "The CompanyEssentials Id is already Present",//5
                "Company Essentials not updated"//6
            };

        }
        #endregion
    }
}
