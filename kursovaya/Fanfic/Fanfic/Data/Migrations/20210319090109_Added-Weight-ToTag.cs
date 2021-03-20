using Microsoft.EntityFrameworkCore.Migrations;

namespace Fanfic.Data.Migrations
{
    public partial class AddedWeightToTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Tags");
        }
    }
}
