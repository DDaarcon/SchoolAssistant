using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAssistant.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CanAccessConfiguration = table.Column<bool>(type: "bit", nullable: false),
                    CanViewAllClassesData = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Current = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentRegisterRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstParent_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstParent_SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstParent_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstParent_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstParent_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstParent_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondParent_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondParent_SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondParent_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondParent_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondParent_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondParent_Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRegisterRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PupilsId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSecondParent = table.Column<bool>(type: "bit", nullable: false),
                    ChildInfoId = table.Column<long>(type: "bigint", nullable: false),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parents_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parents_StudentRegisterRecord_ChildInfoId",
                        column: x => x.ChildInfoId,
                        principalTable: "StudentRegisterRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "OrganizationalClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorId = table.Column<long>(type: "bigint", nullable: false),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationalClass_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalClass_Teachers_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherToAdditionalSubject",
                columns: table => new
                {
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TeacherToAdditionalSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherToAdditionalSubject_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherToMainSubject",
                columns: table => new
                {
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TeacherToMainSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherToMainSubject_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarksOfClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    OrganizationalClassId = table.Column<long>(type: "bigint", nullable: true),
                    SubjectClassId = table.Column<long>(type: "bigint", nullable: true),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarksOfClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarksOfClass_OrganizationalClass_OrganizationalClassId",
                        column: x => x.OrganizationalClassId,
                        principalTable: "OrganizationalClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarksOfClass_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarksOfClass_SubjectClass_SubjectClassId",
                        column: x => x.SubjectClassId,
                        principalTable: "SubjectClass",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PeriodicLesson",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CronPeriodicity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomDuration = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    ParticipatingOrganizationalClassId = table.Column<long>(type: "bigint", nullable: true),
                    ParticipatingSubjectClassId = table.Column<long>(type: "bigint", nullable: true),
                    LecturerId = table.Column<long>(type: "bigint", nullable: false),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicLesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodicLesson_OrganizationalClass_ParticipatingOrganizationalClassId",
                        column: x => x.ParticipatingOrganizationalClassId,
                        principalTable: "OrganizationalClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PeriodicLesson_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeriodicLesson_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PeriodicLesson_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeriodicLesson_SubjectClass_ParticipatingSubjectClassId",
                        column: x => x.ParticipatingSubjectClassId,
                        principalTable: "SubjectClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PeriodicLesson_Teachers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumerInJurnal = table.Column<int>(type: "int", nullable: false),
                    InfoId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationalClassId = table.Column<long>(type: "bigint", nullable: true),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_OrganizationalClass_OrganizationalClassId",
                        column: x => x.OrganizationalClassId,
                        principalTable: "OrganizationalClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Students_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Students_StudentRegisterRecord_InfoId",
                        column: x => x.InfoId,
                        principalTable: "StudentRegisterRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_PeriodicLesson_FromScheduleId",
                        column: x => x.FromScheduleId,
                        principalTable: "PeriodicLesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lesson_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: true),
                    TeacherId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mark",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    IssuerId = table.Column<long>(type: "bigint", nullable: false),
                    CollectionId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Main = table.Column<int>(type: "int", nullable: false),
                    Prefix = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mark_MarksOfClass_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "MarksOfClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mark_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mark_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mark_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mark_Teachers_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjectClass",
                columns: table => new
                {
                    StudentsId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectClassesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjectClass", x => new { x.StudentsId, x.SubjectClassesId });
                    table.ForeignKey(
                        name: "FK_StudentSubjectClass_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjectClass_SubjectClass_SubjectClassesId",
                        column: x => x.SubjectClassesId,
                        principalTable: "SubjectClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attendance_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeacherId",
                table: "AspNetUsers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_LessonId",
                table: "Attendance",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_SemesterId",
                table: "Attendance",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendance",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_FromScheduleId",
                table: "Lesson",
                column: "FromScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_SemesterId",
                table: "Lesson",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_CollectionId",
                table: "Mark",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_IssuerId",
                table: "Mark",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_SemesterId",
                table: "Mark",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_StudentId",
                table: "Mark",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_SubjectId",
                table: "Mark",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksOfClass_OrganizationalClassId",
                table: "MarksOfClass",
                column: "OrganizationalClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksOfClass_SemesterId",
                table: "MarksOfClass",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksOfClass_SubjectClassId",
                table: "MarksOfClass",
                column: "SubjectClassId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalClass_SemesterId",
                table: "OrganizationalClass",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalClass_SupervisorId",
                table: "OrganizationalClass",
                column: "SupervisorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_ChildInfoId",
                table: "Parents",
                column: "ChildInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_SemesterId",
                table: "Parents",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicLesson_LecturerId",
                table: "PeriodicLesson",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicLesson_ParticipatingOrganizationalClassId",
                table: "PeriodicLesson",
                column: "ParticipatingOrganizationalClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicLesson_ParticipatingSubjectClassId",
                table: "PeriodicLesson",
                column: "ParticipatingSubjectClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicLesson_RoomId",
                table: "PeriodicLesson",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicLesson_SemesterId",
                table: "PeriodicLesson",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicLesson_SubjectId",
                table: "PeriodicLesson",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InfoId",
                table: "Students",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_OrganizationalClassId",
                table: "Students",
                column: "OrganizationalClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SemesterId",
                table: "Students",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectClass_SubjectClassesId",
                table: "StudentSubjectClass",
                column: "SubjectClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClass_SemesterId",
                table: "SubjectClass",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClass_SubjectId",
                table: "SubjectClass",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherToAdditionalSubject_SubjectId",
                table: "TeacherToAdditionalSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherToAdditionalSubject_TeacherId",
                table: "TeacherToAdditionalSubject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherToMainSubject_SubjectId",
                table: "TeacherToMainSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherToMainSubject_TeacherId",
                table: "TeacherToMainSubject",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Mark");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "StudentSubjectClass");

            migrationBuilder.DropTable(
                name: "TeacherToAdditionalSubject");

            migrationBuilder.DropTable(
                name: "TeacherToMainSubject");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "MarksOfClass");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "PeriodicLesson");

            migrationBuilder.DropTable(
                name: "StudentRegisterRecord");

            migrationBuilder.DropTable(
                name: "OrganizationalClass");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "SubjectClass");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
