using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class Fixed_table_names_and_removed_unnecessary_teacher_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Lesson_LessonId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_PeriodicLesson_FromScheduleId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_SchoolYears_SchoolYearId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_MarksOfClass_CollectionId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_SchoolYears_SchoolYearId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Students_StudentId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Subject_SubjectId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Teachers_IssuerId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksOfClass_OrganizationalClass_OrganizationalClassId",
                table: "MarksOfClass");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksOfClass_SchoolYears_SchoolYearId",
                table: "MarksOfClass");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksOfClass_SubjectClass_SubjectClassId",
                table: "MarksOfClass");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClass_SchoolYears_SchoolYearId",
                table: "OrganizationalClass");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClass_Teachers_SupervisorId",
                table: "OrganizationalClass");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLesson_OrganizationalClass_ParticipatingOrganizationalClassId",
                table: "PeriodicLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLesson_Room_RoomId",
                table: "PeriodicLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLesson_SchoolYears_SchoolYearId",
                table: "PeriodicLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLesson_Subject_SubjectId",
                table: "PeriodicLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLesson_SubjectClass_ParticipatingSubjectClassId",
                table: "PeriodicLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLesson_Teachers_LecturerId",
                table: "PeriodicLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_OrganizationalClass_OrganizationalClassId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectClass_SubjectClass_SubjectClassesId",
                table: "StudentSubjectClass");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClass_OrganizationalClass_OrganizationalClassId",
                table: "SubjectClass");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClass_SchoolYears_SchoolYearId",
                table: "SubjectClass");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClass_Subject_SubjectId",
                table: "SubjectClass");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClass_Teachers_TeacherId",
                table: "SubjectClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectClass",
                table: "SubjectClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeriodicLesson",
                table: "PeriodicLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationalClass",
                table: "OrganizationalClass");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationalClass_SupervisorId",
                table: "OrganizationalClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarksOfClass",
                table: "MarksOfClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mark",
                table: "Mark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "PupilsId",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "SubjectClass",
                newName: "SubjectClasses");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "PeriodicLesson",
                newName: "PeriodicLessons");

            migrationBuilder.RenameTable(
                name: "OrganizationalClass",
                newName: "OrganizationalClasses");

            migrationBuilder.RenameTable(
                name: "MarksOfClass",
                newName: "MarksOfClasses");

            migrationBuilder.RenameTable(
                name: "Mark",
                newName: "Marks");

            migrationBuilder.RenameTable(
                name: "Lesson",
                newName: "Lessons");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectClass_TeacherId",
                table: "SubjectClasses",
                newName: "IX_SubjectClasses_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectClass_SubjectId",
                table: "SubjectClasses",
                newName: "IX_SubjectClasses_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectClass_SchoolYearId",
                table: "SubjectClasses",
                newName: "IX_SubjectClasses_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectClass_OrganizationalClassId",
                table: "SubjectClasses",
                newName: "IX_SubjectClasses_OrganizationalClassId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLesson_SubjectId",
                table: "PeriodicLessons",
                newName: "IX_PeriodicLessons_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLesson_SchoolYearId",
                table: "PeriodicLessons",
                newName: "IX_PeriodicLessons_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLesson_RoomId",
                table: "PeriodicLessons",
                newName: "IX_PeriodicLessons_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLesson_ParticipatingSubjectClassId",
                table: "PeriodicLessons",
                newName: "IX_PeriodicLessons_ParticipatingSubjectClassId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLesson_ParticipatingOrganizationalClassId",
                table: "PeriodicLessons",
                newName: "IX_PeriodicLessons_ParticipatingOrganizationalClassId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLesson_LecturerId",
                table: "PeriodicLessons",
                newName: "IX_PeriodicLessons_LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationalClass_SchoolYearId",
                table: "OrganizationalClasses",
                newName: "IX_OrganizationalClasses_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_MarksOfClass_SubjectClassId",
                table: "MarksOfClasses",
                newName: "IX_MarksOfClasses_SubjectClassId");

            migrationBuilder.RenameIndex(
                name: "IX_MarksOfClass_SchoolYearId",
                table: "MarksOfClasses",
                newName: "IX_MarksOfClasses_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_MarksOfClass_OrganizationalClassId",
                table: "MarksOfClasses",
                newName: "IX_MarksOfClasses_OrganizationalClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_SubjectId",
                table: "Marks",
                newName: "IX_Marks_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_StudentId",
                table: "Marks",
                newName: "IX_Marks_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_SchoolYearId",
                table: "Marks",
                newName: "IX_Marks_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_IssuerId",
                table: "Marks",
                newName: "IX_Marks_IssuerId");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_CollectionId",
                table: "Marks",
                newName: "IX_Marks_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_SchoolYearId",
                table: "Lessons",
                newName: "IX_Lessons_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_FromScheduleId",
                table: "Lessons",
                newName: "IX_Lessons_FromScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectClasses",
                table: "SubjectClasses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeriodicLessons",
                table: "PeriodicLessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationalClasses",
                table: "OrganizationalClasses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarksOfClasses",
                table: "MarksOfClasses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marks",
                table: "Marks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalClasses_SupervisorId",
                table: "OrganizationalClasses",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Lessons_LessonId",
                table: "Attendance",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_PeriodicLessons_FromScheduleId",
                table: "Lessons",
                column: "FromScheduleId",
                principalTable: "PeriodicLessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_SchoolYears_SchoolYearId",
                table: "Lessons",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_MarksOfClasses_CollectionId",
                table: "Marks",
                column: "CollectionId",
                principalTable: "MarksOfClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_SchoolYears_SchoolYearId",
                table: "Marks",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Students_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Subject_SubjectId",
                table: "Marks",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Teachers_IssuerId",
                table: "Marks",
                column: "IssuerId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarksOfClasses_OrganizationalClasses_OrganizationalClassId",
                table: "MarksOfClasses",
                column: "OrganizationalClassId",
                principalTable: "OrganizationalClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksOfClasses_SchoolYears_SchoolYearId",
                table: "MarksOfClasses",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksOfClasses_SubjectClasses_SubjectClassId",
                table: "MarksOfClasses",
                column: "SubjectClassId",
                principalTable: "SubjectClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClasses_SchoolYears_SchoolYearId",
                table: "OrganizationalClasses",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClasses_Teachers_SupervisorId",
                table: "OrganizationalClasses",
                column: "SupervisorId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLessons_OrganizationalClasses_ParticipatingOrganizationalClassId",
                table: "PeriodicLessons",
                column: "ParticipatingOrganizationalClassId",
                principalTable: "OrganizationalClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLessons_Rooms_RoomId",
                table: "PeriodicLessons",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLessons_SchoolYears_SchoolYearId",
                table: "PeriodicLessons",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLessons_Subject_SubjectId",
                table: "PeriodicLessons",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLessons_SubjectClasses_ParticipatingSubjectClassId",
                table: "PeriodicLessons",
                column: "ParticipatingSubjectClassId",
                principalTable: "SubjectClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLessons_Teachers_LecturerId",
                table: "PeriodicLessons",
                column: "LecturerId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_OrganizationalClasses_OrganizationalClassId",
                table: "Students",
                column: "OrganizationalClassId",
                principalTable: "OrganizationalClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectClass_SubjectClasses_SubjectClassesId",
                table: "StudentSubjectClass",
                column: "SubjectClassesId",
                principalTable: "SubjectClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClasses_OrganizationalClasses_OrganizationalClassId",
                table: "SubjectClasses",
                column: "OrganizationalClassId",
                principalTable: "OrganizationalClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClasses_SchoolYears_SchoolYearId",
                table: "SubjectClasses",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClasses_Subject_SubjectId",
                table: "SubjectClasses",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClasses_Teachers_TeacherId",
                table: "SubjectClasses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Lessons_LessonId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_PeriodicLessons_FromScheduleId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_SchoolYears_SchoolYearId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_MarksOfClasses_CollectionId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_SchoolYears_SchoolYearId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Students_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Subject_SubjectId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Teachers_IssuerId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksOfClasses_OrganizationalClasses_OrganizationalClassId",
                table: "MarksOfClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksOfClasses_SchoolYears_SchoolYearId",
                table: "MarksOfClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksOfClasses_SubjectClasses_SubjectClassId",
                table: "MarksOfClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClasses_SchoolYears_SchoolYearId",
                table: "OrganizationalClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalClasses_Teachers_SupervisorId",
                table: "OrganizationalClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLessons_OrganizationalClasses_ParticipatingOrganizationalClassId",
                table: "PeriodicLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLessons_Rooms_RoomId",
                table: "PeriodicLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLessons_SchoolYears_SchoolYearId",
                table: "PeriodicLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLessons_Subject_SubjectId",
                table: "PeriodicLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLessons_SubjectClasses_ParticipatingSubjectClassId",
                table: "PeriodicLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodicLessons_Teachers_LecturerId",
                table: "PeriodicLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_OrganizationalClasses_OrganizationalClassId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectClass_SubjectClasses_SubjectClassesId",
                table: "StudentSubjectClass");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClasses_OrganizationalClasses_OrganizationalClassId",
                table: "SubjectClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClasses_SchoolYears_SchoolYearId",
                table: "SubjectClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClasses_Subject_SubjectId",
                table: "SubjectClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClasses_Teachers_TeacherId",
                table: "SubjectClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectClasses",
                table: "SubjectClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeriodicLessons",
                table: "PeriodicLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationalClasses",
                table: "OrganizationalClasses");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationalClasses_SupervisorId",
                table: "OrganizationalClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarksOfClasses",
                table: "MarksOfClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marks",
                table: "Marks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "SubjectClasses",
                newName: "SubjectClass");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "PeriodicLessons",
                newName: "PeriodicLesson");

            migrationBuilder.RenameTable(
                name: "OrganizationalClasses",
                newName: "OrganizationalClass");

            migrationBuilder.RenameTable(
                name: "MarksOfClasses",
                newName: "MarksOfClass");

            migrationBuilder.RenameTable(
                name: "Marks",
                newName: "Mark");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "Lesson");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectClasses_TeacherId",
                table: "SubjectClass",
                newName: "IX_SubjectClass_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectClasses_SubjectId",
                table: "SubjectClass",
                newName: "IX_SubjectClass_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectClasses_SchoolYearId",
                table: "SubjectClass",
                newName: "IX_SubjectClass_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectClasses_OrganizationalClassId",
                table: "SubjectClass",
                newName: "IX_SubjectClass_OrganizationalClassId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLessons_SubjectId",
                table: "PeriodicLesson",
                newName: "IX_PeriodicLesson_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLessons_SchoolYearId",
                table: "PeriodicLesson",
                newName: "IX_PeriodicLesson_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLessons_RoomId",
                table: "PeriodicLesson",
                newName: "IX_PeriodicLesson_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLessons_ParticipatingSubjectClassId",
                table: "PeriodicLesson",
                newName: "IX_PeriodicLesson_ParticipatingSubjectClassId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLessons_ParticipatingOrganizationalClassId",
                table: "PeriodicLesson",
                newName: "IX_PeriodicLesson_ParticipatingOrganizationalClassId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodicLessons_LecturerId",
                table: "PeriodicLesson",
                newName: "IX_PeriodicLesson_LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationalClasses_SchoolYearId",
                table: "OrganizationalClass",
                newName: "IX_OrganizationalClass_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_MarksOfClasses_SubjectClassId",
                table: "MarksOfClass",
                newName: "IX_MarksOfClass_SubjectClassId");

            migrationBuilder.RenameIndex(
                name: "IX_MarksOfClasses_SchoolYearId",
                table: "MarksOfClass",
                newName: "IX_MarksOfClass_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_MarksOfClasses_OrganizationalClassId",
                table: "MarksOfClass",
                newName: "IX_MarksOfClass_OrganizationalClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_SubjectId",
                table: "Mark",
                newName: "IX_Mark_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_StudentId",
                table: "Mark",
                newName: "IX_Mark_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_SchoolYearId",
                table: "Mark",
                newName: "IX_Mark_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_IssuerId",
                table: "Mark",
                newName: "IX_Mark_IssuerId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_CollectionId",
                table: "Mark",
                newName: "IX_Mark_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_SchoolYearId",
                table: "Lesson",
                newName: "IX_Lesson_SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_FromScheduleId",
                table: "Lesson",
                newName: "IX_Lesson_FromScheduleId");

            migrationBuilder.AddColumn<long>(
                name: "PupilsId",
                table: "Teachers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectClass",
                table: "SubjectClass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeriodicLesson",
                table: "PeriodicLesson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationalClass",
                table: "OrganizationalClass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarksOfClass",
                table: "MarksOfClass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mark",
                table: "Mark",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalClass_SupervisorId",
                table: "OrganizationalClass",
                column: "SupervisorId",
                unique: true,
                filter: "[SupervisorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Lesson_LessonId",
                table: "Attendance",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_PeriodicLesson_FromScheduleId",
                table: "Lesson",
                column: "FromScheduleId",
                principalTable: "PeriodicLesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_SchoolYears_SchoolYearId",
                table: "Lesson",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_MarksOfClass_CollectionId",
                table: "Mark",
                column: "CollectionId",
                principalTable: "MarksOfClass",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_SchoolYears_SchoolYearId",
                table: "Mark",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Students_StudentId",
                table: "Mark",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Subject_SubjectId",
                table: "Mark",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Teachers_IssuerId",
                table: "Mark",
                column: "IssuerId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarksOfClass_OrganizationalClass_OrganizationalClassId",
                table: "MarksOfClass",
                column: "OrganizationalClassId",
                principalTable: "OrganizationalClass",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksOfClass_SchoolYears_SchoolYearId",
                table: "MarksOfClass",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksOfClass_SubjectClass_SubjectClassId",
                table: "MarksOfClass",
                column: "SubjectClassId",
                principalTable: "SubjectClass",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClass_SchoolYears_SchoolYearId",
                table: "OrganizationalClass",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalClass_Teachers_SupervisorId",
                table: "OrganizationalClass",
                column: "SupervisorId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLesson_OrganizationalClass_ParticipatingOrganizationalClassId",
                table: "PeriodicLesson",
                column: "ParticipatingOrganizationalClassId",
                principalTable: "OrganizationalClass",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLesson_Room_RoomId",
                table: "PeriodicLesson",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLesson_SchoolYears_SchoolYearId",
                table: "PeriodicLesson",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLesson_Subject_SubjectId",
                table: "PeriodicLesson",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLesson_SubjectClass_ParticipatingSubjectClassId",
                table: "PeriodicLesson",
                column: "ParticipatingSubjectClassId",
                principalTable: "SubjectClass",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodicLesson_Teachers_LecturerId",
                table: "PeriodicLesson",
                column: "LecturerId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_OrganizationalClass_OrganizationalClassId",
                table: "Students",
                column: "OrganizationalClassId",
                principalTable: "OrganizationalClass",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectClass_SubjectClass_SubjectClassesId",
                table: "StudentSubjectClass",
                column: "SubjectClassesId",
                principalTable: "SubjectClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClass_OrganizationalClass_OrganizationalClassId",
                table: "SubjectClass",
                column: "OrganizationalClassId",
                principalTable: "OrganizationalClass",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClass_SchoolYears_SchoolYearId",
                table: "SubjectClass",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClass_Subject_SubjectId",
                table: "SubjectClass",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClass_Teachers_TeacherId",
                table: "SubjectClass",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
