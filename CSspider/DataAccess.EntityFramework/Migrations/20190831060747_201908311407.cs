using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.EntityFramework.Migrations
{
    public partial class _201908311407 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Stocks_StockID",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "StockID",
                table: "Quotes",
                newName: "DividendID");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_StockID",
                table: "Quotes",
                newName: "IX_Quotes_DividendID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Dividends_DividendID",
                table: "Quotes",
                column: "DividendID",
                principalTable: "Dividends",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Dividends_DividendID",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "DividendID",
                table: "Quotes",
                newName: "StockID");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_DividendID",
                table: "Quotes",
                newName: "IX_Quotes_StockID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Stocks_StockID",
                table: "Quotes",
                column: "StockID",
                principalTable: "Stocks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
