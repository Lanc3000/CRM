using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CRMCore.Migrations
{
    public partial class FInance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FinanceType = table.Column<int>(nullable: false),
                    SubTypeId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Place = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    CreatedId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finances_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finances_FinanceSubTypes_SubTypeId",
                        column: x => x.SubTypeId,
                        principalTable: "FinanceSubTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finances_ProjectId",
                table: "Finances",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Finances_SubTypeId",
                table: "Finances",
                column: "SubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Finances_UserId",
                table: "Finances",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finances");
        }
    }
}
