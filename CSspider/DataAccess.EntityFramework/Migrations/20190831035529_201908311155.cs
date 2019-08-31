using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.EntityFramework.Migrations
{
    public partial class _201908311155 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DividendDay",
                table: "Dividends",
                newName: "DividendDate");

            migrationBuilder.AddColumn<string>(
                name: "RateOfReturn",
                table: "Dividends",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RateOfReturn",
                table: "Dividends");

            migrationBuilder.RenameColumn(
                name: "DividendDate",
                table: "Dividends",
                newName: "DividendDay");
        }
    }
}
