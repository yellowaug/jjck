using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.EntityFramework.Migrations
{
    public partial class _201908310837 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Stocks");

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartPrice = table.Column<decimal>(nullable: false),
                    EndPrice = table.Column<decimal>(nullable: false),
                    MaxPrice = table.Column<decimal>(nullable: false),
                    MinPrice = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Volume = table.Column<double>(nullable: false),
                    AMO = table.Column<decimal>(nullable: false),
                    TurnoverRate = table.Column<string>(nullable: true),
                    StockID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Quotes_Stocks_StockID",
                        column: x => x.StockID,
                        principalTable: "Stocks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_StockID",
                table: "Quotes",
                column: "StockID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Stocks",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
