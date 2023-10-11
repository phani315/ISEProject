using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaniniInvestmentSearchEngineCompanyMarketEssentials.Migrations
{
    public partial class Essebtials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyEssentials",
                columns: table => new
                {
                    EssenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    MarketCap = table.Column<double>(type: "float", nullable: false),
                    EnterpriceValue = table.Column<double>(type: "float", nullable: false),
                    NoOfShares = table.Column<double>(type: "float", nullable: false),
                    DivYield = table.Column<double>(type: "float", nullable: false),
                    Cash = table.Column<double>(type: "float", nullable: false),
                    PromoterHolding = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BookValue = table.Column<double>(type: "float", nullable: false),
                    PriceToBook = table.Column<double>(type: "float", nullable: true),
                    PriceToEarning = table.Column<double>(type: "float", nullable: true),
                    Eps = table.Column<double>(type: "float", nullable: true),
                    NetIncome = table.Column<double>(type: "float", nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEssentials", x => x.EssenID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyEssentials");
        }
    }
}
