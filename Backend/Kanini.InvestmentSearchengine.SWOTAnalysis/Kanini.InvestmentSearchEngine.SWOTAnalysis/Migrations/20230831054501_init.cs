using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SWOT",
                columns: table => new
                {
                    SwotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SWOT", x => x.SwotId);
                });

            migrationBuilder.CreateTable(
                name: "Oppurtunities",
                columns: table => new
                {
                    OppurtunityId = table.Column<int>(type: "int", nullable: false),
                    OppurtunityValue = table.Column<int>(type: "int", nullable: false),
                    OppurtunityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oppurtunities", x => x.OppurtunityId);
                    table.ForeignKey(
                        name: "FK_Oppurtunities_SWOT_OppurtunityId",
                        column: x => x.OppurtunityId,
                        principalTable: "SWOT",
                        principalColumn: "SwotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strengths",
                columns: table => new
                {
                    StrengthId = table.Column<int>(type: "int", nullable: false),
                    StrengthValue = table.Column<int>(type: "int", nullable: false),
                    StrengthDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strengths", x => x.StrengthId);
                    table.ForeignKey(
                        name: "FK_Strengths_SWOT_StrengthId",
                        column: x => x.StrengthId,
                        principalTable: "SWOT",
                        principalColumn: "SwotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Threats",
                columns: table => new
                {
                    ThreatId = table.Column<int>(type: "int", nullable: false),
                    ThreatValue = table.Column<int>(type: "int", nullable: false),
                    ThreatDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threats", x => x.ThreatId);
                    table.ForeignKey(
                        name: "FK_Threats_SWOT_ThreatId",
                        column: x => x.ThreatId,
                        principalTable: "SWOT",
                        principalColumn: "SwotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weaknesses",
                columns: table => new
                {
                    WeaknessId = table.Column<int>(type: "int", nullable: false),
                    WeaknessValue = table.Column<int>(type: "int", nullable: false),
                    WeaknessDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weaknesses", x => x.WeaknessId);
                    table.ForeignKey(
                        name: "FK_Weaknesses_SWOT_WeaknessId",
                        column: x => x.WeaknessId,
                        principalTable: "SWOT",
                        principalColumn: "SwotId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oppurtunities");

            migrationBuilder.DropTable(
                name: "Strengths");

            migrationBuilder.DropTable(
                name: "Threats");

            migrationBuilder.DropTable(
                name: "Weaknesses");

            migrationBuilder.DropTable(
                name: "SWOT");
        }
    }
}
