using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class User_fix_and_Email_StudentRegisterRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Students_StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_SchoolYears_SchoolYearId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_StudentRegisterRecord_ChildInfoId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_ChildInfoId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_SchoolYearId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "ChildInfoId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "IsSecondParent",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "Parents");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "StudentRegisterRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParentToChild",
                columns: table => new
                {
                    IsSecondParent = table.Column<bool>(type: "bit", nullable: false),
                    ChildInfoId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentToChild", x => new { x.ChildInfoId, x.IsSecondParent });
                    table.ForeignKey(
                        name: "FK_ParentToChild_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParentToChild_StudentRegisterRecord_ChildInfoId",
                        column: x => x.ChildInfoId,
                        principalTable: "StudentRegisterRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentId",
                table: "AspNetUsers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentToChild_ParentId",
                table: "ParentToChild",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Parents_ParentId",
                table: "AspNetUsers",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StudentRegisterRecord_StudentId",
                table: "AspNetUsers",
                column: "StudentId",
                principalTable: "StudentRegisterRecord",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Parents_ParentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudentRegisterRecord_StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ParentToChild");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "StudentRegisterRecord");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "ChildInfoId",
                table: "Parents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsSecondParent",
                table: "Parents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "SchoolYearId",
                table: "Parents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_ChildInfoId",
                table: "Parents",
                column: "ChildInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_SchoolYearId",
                table: "Parents",
                column: "SchoolYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Students_StudentId",
                table: "AspNetUsers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_SchoolYears_SchoolYearId",
                table: "Parents",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_StudentRegisterRecord_ChildInfoId",
                table: "Parents",
                column: "ChildInfoId",
                principalTable: "StudentRegisterRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
