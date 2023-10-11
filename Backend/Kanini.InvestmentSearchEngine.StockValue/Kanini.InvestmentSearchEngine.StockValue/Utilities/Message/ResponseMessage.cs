using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.StockValue.Utilities.Message
{
    [ExcludeFromCodeCoverage]
    public class ResponseMessage
    {
        public static List<string> Messages;
        static ResponseMessage()
        {
            Messages = new List<string>
            {
                "Something went wrong",//0
                "Server not working",//1
                "StockPrice not found",//2
                "StockTransaction not found",//3
                "Context is emyty",//4
                "Id should be unassigned",//5
                "Unable to add stock price",//6
                "No stock prices are available",//7
                "Unable to update stock price",//8
                "Unable to delete stock price",//9
                "Unable to access the database for stock price information.",//10
                "No Stockvalue is available with the given Id",//11
                "Adding stockvalue details failed!!!",//12
                "Updating stockvalue details failed!!!",//13
                "Unable to access the database for stock transaction information.",//14
                "Updating stocktransaction details failed!!!",//15
                "No stock transactions are available",//16
                "No stocktransaction is available with the given Id",//17
                "Unable to update current stock price",//18
                "No current stock price details are available with the given company Id",//19
                "The company details are already added to the database"//20
            };

        }
    }
}
