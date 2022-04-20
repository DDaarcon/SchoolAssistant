using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class Some_new_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClass_Teachers_SupervisorId",
                table: "OrganizationalClass");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationalClass_SupervisorId",
                table: "OrganizationalClass");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationalClassId",
                table: "SubjectClass",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TeacherId",
                table: "SubjectClass",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SupervisorId",
                table: "OrganizationalClass",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Distinction",
                table: "OrganizationalClass",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "OrganizationalClass",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "OrganizationalClass",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClass_OrganizationalClassId",
                table: "SubjectClass",
                column: "OrganizationalClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClass_TeacherId",
                table: "SubjectClass",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalClass_SupervisorId",
                table: "OrganizationalClass",
                column: "SupervisorId",
                unique: true,
                filter: "[SupervisorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClass_Teachers_SupervisorId",
                table: "OrganizationalClass",
                column: "SupervisorId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClass_OrganizationalClass_OrganizationalClassId",
                table: "SubjectClass",
                column: "OrganizationalClassId",
                principalTable: "OrganizationalClass",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClass_Teachers_TeacherId",
                table: "SubjectClass",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClass_Teachers_SupervisorId",
                table: "OrganizationalClass");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClass_OrganizationalClass_OrganizationalClassId",
                table: "SubjectClass");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClass_Teachers_TeacherId",
                table: "SubjectClass");

            migrationBuilder.DropIndex(
                name: "IX_SubjectClass_OrganizationalClassId",
                table: "SubjectClass");

            migrationBuilder.DropIndex(
                name: "IX_SubjectClass_TeacherId",
                table: "SubjectClass");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationalClass_SupervisorId",
                table: "OrganizationalClass");

            migrationBuilder.DropColumn(
                name: "OrganizationalClassId",
                table: "SubjectClass");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SubjectClass");

            migrationBuilder.DropColumn(
                name: "Distinction",
                table: "OrganizationalClass");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "OrganizationalClass");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "OrganizationalClass");

            migrationBuilder.AlterColumn<long>(
                name: "SupervisorId",
                table: "OrganizationalClass",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalClass_SupervisorId",
                table: "OrganizationalClass",
                column: "SupervisorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClass_Teachers_SupervisorId",
                table: "OrganizationalClass",
                column: "SupervisorId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
