using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMCore.Migrations
{
    public partial class FileUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "FileData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "FileData",
                nullable: true);
        }
    }
}
