using Microsoft.EntityFrameworkCore.Migrations;

namespace Fanfic.Data.Migrations
{
    public partial class TagsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTale_Tag_TagsId",
                table: "TagTale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagTale",
                table: "TagTale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "TagTale");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "TagsName",
                table: "TagTale",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagTale",
                table: "TagTale",
                columns: new[] { "TagsName", "TalesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTale_Tags_TagsName",
                table: "TagTale",
                column: "TagsName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTale_Tags_TagsName",
                table: "TagTale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagTale",
                table: "TagTale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagsName",
                table: "TagTale");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.AddColumn<long>(
                name: "TagsId",
                table: "TagTale",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Tag",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagTale",
                table: "TagTale",
                columns: new[] { "TagsId", "TalesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTale_Tag_TagsId",
                table: "TagTale",
                column: "TagsId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
