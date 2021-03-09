using Microsoft.EntityFrameworkCore.Migrations;

namespace Fanfic.Data.Migrations
{
    public partial class ChapterUserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Chapters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_UserId",
                table: "Chapters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_AspNetUsers_UserId",
                table: "Chapters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_AspNetUsers_UserId",
                table: "Chapters");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_UserId",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chapters");
        }
    }
}
