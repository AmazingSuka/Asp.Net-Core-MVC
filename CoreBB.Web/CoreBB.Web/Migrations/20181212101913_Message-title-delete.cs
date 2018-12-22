using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreBB.Web.Migrations
{
    public partial class Messagetitledelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Message",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
