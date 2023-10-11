using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanini.InvestmentSearchEngine.FinstarRating.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finstar",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    TotalRating = table.Column<double>(type: "float", nullable: false),
                    TotalReviewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finstar", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "Efficiency",
                columns: table => new
                {
                    EfficiencyId = table.Column<int>(type: "int", nullable: false),
                    EfficiencyRate = table.Column<double>(type: "float", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Efficiency", x => x.EfficiencyId);
                    table.ForeignKey(
                        name: "FK_Efficiency_Finstar_EfficiencyId",
                        column: x => x.EfficiencyId,
                        principalTable: "Finstar",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Financial",
                columns: table => new
                {
                    FinancialID = table.Column<int>(type: "int", nullable: false),
                    FinancialRate = table.Column<double>(type: "float", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financial", x => x.FinancialID);
                    table.ForeignKey(
                        name: "FK_Financial_Finstar_FinancialID",
                        column: x => x.FinancialID,
                        principalTable: "Finstar",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerShip",
                columns: table => new
                {
                    OwnerShipID = table.Column<int>(type: "int", nullable: false),
                    OwnerShipRate = table.Column<double>(type: "float", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerShip", x => x.OwnerShipID);
                    table.ForeignKey(
                        name: "FK_OwnerShip_Finstar_OwnerShipID",
                        column: x => x.OwnerShipID,
                        principalTable: "Finstar",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valuation",
                columns: table => new
                {
                    ValuationID = table.Column<int>(type: "int", nullable: false),
                    ValuationRate = table.Column<double>(type: "float", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valuation", x => x.ValuationID);
                    table.ForeignKey(
                        name: "FK_Valuation_Finstar_ValuationID",
                        column: x => x.ValuationID,
                        principalTable: "Finstar",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Efficiency");

            migrationBuilder.DropTable(
                name: "Financial");

            migrationBuilder.DropTable(
                name: "OwnerShip");

            migrationBuilder.DropTable(
                name: "Valuation");

            migrationBuilder.DropTable(
                name: "Finstar");
        }
    }
}
