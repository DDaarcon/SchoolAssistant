using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class Rename_SchoolYear_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Semesters_SchoolYearId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Semesters_SchoolYearId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Semesters_SchoolYearId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksOfClass_Semesters_SchoolYearId",
                table: "MarksOfClass");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClass_Semesters_SchoolYearId",
                table: "OrganizationalClass");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Semesters_SchoolYearId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLesson_Semesters_SchoolYearId",
                table: "PeriodicLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semesters_SchoolYearId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClass_Semesters_SchoolYearId",
                table: "SubjectClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.RenameTable(
                name: "Semesters",
                newName: "SchoolYears");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolYears",
                table: "SchoolYears",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_SchoolYears_SchoolYearId",
                table: "Attendance",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_SchoolYears_SchoolYearId",
                table: "Lesson",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_SchoolYears_SchoolYearId",
                table: "Mark",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksOfClass_SchoolYears_SchoolYearId",
                table: "MarksOfClass",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClass_SchoolYears_SchoolYearId",
                table: "OrganizationalClass",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_SchoolYears_SchoolYearId",
                table: "Parents",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLesson_SchoolYears_SchoolYearId",
                table: "PeriodicLesson",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SchoolYears_SchoolYearId",
                table: "Students",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClass_SchoolYears_SchoolYearId",
                table: "SubjectClass",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_SchoolYears_SchoolYearId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_SchoolYears_SchoolYearId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_SchoolYears_SchoolYearId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksOfClass_SchoolYears_SchoolYearId",
                table: "MarksOfClass");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClass_SchoolYears_SchoolYearId",
                table: "OrganizationalClass");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_SchoolYears_SchoolYearId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLesson_SchoolYears_SchoolYearId",
                table: "PeriodicLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SchoolYears_SchoolYearId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClass_SchoolYears_SchoolYearId",
                table: "SubjectClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolYears",
                table: "SchoolYears");

            migrationBuilder.RenameTable(
                name: "SchoolYears",
                newName: "Semesters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Semesters_SchoolYearId",
                table: "Attendance",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Semesters_SchoolYearId",
                table: "Lesson",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Semesters_SchoolYearId",
                table: "Mark",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksOfClass_Semesters_SchoolYearId",
                table: "MarksOfClass",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClass_Semesters_SchoolYearId",
                table: "OrganizationalClass",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Semesters_SchoolYearId",
                table: "Parents",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLesson_Semesters_SchoolYearId",
                table: "PeriodicLesson",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semesters_SchoolYearId",
                table: "Students",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClass_Semesters_SchoolYearId",
                table: "SubjectClass",
                column: "SchoolYearId",
                principalTable: "Semesters",
                principalColumn: "Id");
        }
    }
}
