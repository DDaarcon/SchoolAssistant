using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class AppConfiguration_public : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Config",
                table: "_Config");

            migrationBuilder.RenameTable(
                name: "_Config",
                newName: "Config");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Config",
                table: "Config",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Config",
                table: "Config");

            migrationBuilder.RenameTable(
                name: "Config",
                newName: "_Config");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Config",
                table: "_Config",
                column: "Key");
        }
    }
}
