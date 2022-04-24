using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class Teacher_to_Subject_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherToMainSubject",
                table: "TeacherToMainSubject");

            migrationBuilder.DropIndex(
                name: "IX_TeacherToMainSubject_SubjectId",
                table: "TeacherToMainSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherToAdditionalSubject",
                table: "TeacherToAdditionalSubject");

            migrationBuilder.DropIndex(
                name: "IX_TeacherToAdditionalSubject_SubjectId",
                table: "TeacherToAdditionalSubject");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherToMainSubject",
                table: "TeacherToMainSubject",
                columns: new[] { "SubjectId", "TeacherId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherToAdditionalSubject",
                table: "TeacherToAdditionalSubject",
                columns: new[] { "SubjectId", "TeacherId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherToMainSubject_TeacherId",
                table: "TeacherToMainSubject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherToAdditionalSubject_TeacherId",
                table: "TeacherToAdditionalSubject",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherToMainSubject",
                table: "TeacherToMainSubject");

            migrationBuilder.DropIndex(
                name: "IX_TeacherToMainSubject_TeacherId",
                table: "TeacherToMainSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherToAdditionalSubject",
                table: "TeacherToAdditionalSubject");

            migrationBuilder.DropIndex(
                name: "IX_TeacherToAdditionalSubject_TeacherId",
                table: "TeacherToAdditionalSubject");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherToMainSubject",
                table: "TeacherToMainSubject",
                columns: new[] { "TeacherId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherToAdditionalSubject",
                table: "TeacherToAdditionalSubject",
                columns: new[] { "TeacherId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherToMainSubject_SubjectId",
                table: "TeacherToMainSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherToAdditionalSubject_SubjectId",
                table: "TeacherToAdditionalSubject",
                column: "SubjectId");
        }
    }
}
