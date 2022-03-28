using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class Semester_Property_Configuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClass_Semesters_SemesterId",
                table: "OrganizationalClass");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students");

            migrationBuilder.AddColumn<long>(
                name: "SubjectClassId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubjectClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectClass_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectClass_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_SubjectClassId",
                table: "Students",
                column: "SubjectClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClass_SemesterId",
                table: "SubjectClass",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClass_SubjectId",
                table: "SubjectClass",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClass_Semesters_SemesterId",
                table: "OrganizationalClass",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SubjectClass_SubjectClassId",
                table: "Students",
                column: "SubjectClassId",
                principalTable: "SubjectClass",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClass_Semesters_SemesterId",
                table: "OrganizationalClass");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SubjectClass_SubjectClassId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "SubjectClass");

            migrationBuilder.DropIndex(
                name: "IX_Students_SubjectClassId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SubjectClassId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClass_Semesters_SemesterId",
                table: "OrganizationalClass",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
