using Microsoft.EntityFrameworkCore.Migrations;

namespace Fanfic.Data.Migrations
{
    public partial class CommentsAddedToTale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaleId",
                table: "Comments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TaleId",
                table: "Comments",
                column: "TaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tales_TaleId",
                table: "Comments",
                column: "TaleId",
                principalTable: "Tales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tales_TaleId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TaleId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TaleId",
                table: "Comments");
        }
    }
}
