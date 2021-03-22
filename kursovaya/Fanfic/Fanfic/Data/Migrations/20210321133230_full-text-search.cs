using Microsoft.EntityFrameworkCore.Migrations;

namespace Fanfic.Data.Migrations
{
    public partial class fulltextsearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE FULLTEXT CATALOG ft AS DEFAULT", true);
            migrationBuilder.Sql("CREATE FULLTEXT INDEX ON dbo.Tales(Name) KEY INDEX UI_Tales_Name WITH STOPLIST = SYSTEM", true) ;
            migrationBuilder.Sql("CREATE FULLTEXT INDEX ON dbo.Comments(Message) KEY INDEX UI_Comments_Message WITH STOPLIST = SYSTEM", true);
            migrationBuilder.Sql("CREATE FULLTEXT INDEX ON dbo.Chapters(Text) KEY INDEX UI_Chapters_Text WITH STOPLIST = SYSTEM", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
//Sql("CREATE FULLTEXT CATALOG ft AS DEFAULT", true);
//CREATE FULLTEXT INDEX ON dbo.Tales(Name) KEY INDEX UI_Tales_Name WITH STOPLIST = SYSTEM
//CREATE FULLTEXT INDEX ON dbo.Comments(Message) KEY INDEX UI_Comments_Message WITH STOPLIST = SYSTEM
//CREATE FULLTEXT INDEX ON dbo.Chapters(Text) KEY INDEX UI_Chapters_Text WITH STOPLIST = SYSTEM