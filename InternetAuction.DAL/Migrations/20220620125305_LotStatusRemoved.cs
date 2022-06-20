using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetAuction.DAL.Migrations
{
    public partial class LotStatusRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
