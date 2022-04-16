using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class Teacher_to_subject_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "TeacherToMainSubject",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "TeacherToAdditionalSubject",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherToMainSubject",
                table: "TeacherToMainSubject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherToAdditionalSubject",
                table: "TeacherToAdditionalSubject",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherToMainSubject",
                table: "TeacherToMainSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherToAdditionalSubject",
                table: "TeacherToAdditionalSubject");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeacherToMainSubject");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeacherToAdditionalSubject");
        }
    }
}
