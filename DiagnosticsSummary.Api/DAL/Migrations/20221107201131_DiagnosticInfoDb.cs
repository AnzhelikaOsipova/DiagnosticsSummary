using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosticsSummary.Api.DAL.Migrations
{
    public partial class DiagnosticInfoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiagnosticInfos",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ValueInterpreter = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticInfos", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosticInfos");
        }
    }
}
