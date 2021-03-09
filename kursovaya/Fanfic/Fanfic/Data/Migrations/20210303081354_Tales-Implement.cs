using Microsoft.EntityFrameworkCore.Migrations;

namespace Fanfic.Data.Migrations
{
    public partial class TalesImplement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageRating",
                table: "Tales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ganre",
                table: "Tales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tales",
                type: "nvarchar(70)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberOfRatings",
                table: "Tales",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDiscription",
                table: "Tales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapter_Tales_TaleId",
                        column: x => x.TaleId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagTale",
                columns: table => new
                {
                    TagsId = table.Column<long>(type: "bigint", nullable: false),
                    TalesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTale", x => new { x.TagsId, x.TalesId });
                    table.ForeignKey(
                        name: "FK_TagTale_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTale_Tales_TalesId",
                        column: x => x.TalesId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_TaleId",
                table: "Chapter",
                column: "TaleId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTale_TalesId",
                table: "TagTale",
                column: "TalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropTable(
                name: "TagTale");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Tales");

            migrationBuilder.DropColumn(
                name: "Ganre",
                table: "Tales");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tales");

            migrationBuilder.DropColumn(
                name: "NumberOfRatings",
                table: "Tales");

            migrationBuilder.DropColumn(
                name: "ShortDiscription",
                table: "Tales");
        }
    }
}
