using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMCore.Migrations
{
    public partial class FileAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "FileData",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "FileData",
                newName: "Path");
        }
    }
}
