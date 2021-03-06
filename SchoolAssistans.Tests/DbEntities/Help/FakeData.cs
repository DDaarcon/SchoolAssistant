using Bogus;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Attendance;
using SchoolAssistant.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public class FakeData
    {
        // TODO: creating Parent entities and connecting them to Users

        private static readonly string[] _SubjectNames = new string[] {
            "Art", "Music", "Drama", "Latin", "Sport Science", "Design Technology", "Computer Science", "Biology", "Chemistry", "Physics",
            "History", "Geography", "Economics"
        };

        private static readonly IList<string> _RoomNames = new List<string>
        {
            "art room", "computer room", "library"
        };

        private static Faker<ParentRegisterSubrecord> _ParentRegisterSubrecordFaker => new Faker<ParentRegisterSubrecord>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.SecondName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.Email, (f, reg) => f.Internet.Email(reg.FirstName, reg.LastName));


        private static Faker<StudentRegisterRecord> _StudentRegisterRecordFaker => new Faker<StudentRegisterRecord>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.SecondName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PlaceOfBirth, f => f.Address.City())
            .RuleFor(x => x.PersonalID, f => f.UniqueIndex.ToString())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.DateOfBirth, f => f.Date.BetweenDateOnly(new DateOnly(2002, 1, 1), new DateOnly(2010, 12, 31)))
            .RuleFor(x => x.FirstParent, (f, reg) =>
            {
                var parentFaker = _ParentRegisterSubrecordFaker
                    .RuleFor(x => x.Address, _ => reg.Address);

                return parentFaker.Generate();
            })
            .RuleFor(x => x.SecondParent, (f, reg) =>
            {
                var parentFaker = _ParentRegisterSubrecordFaker
                    .RuleFor(x => x.Address, f => f.PickRandom(reg.Address, f.Address.FullAddress()));

                return f.PickRandom(null, parentFaker.Generate());
            });

        private static Faker<Student> _StudentFaker => new Faker<Student>()
            .RuleFor(x => x.NumberInJournal, f => f.Random.Int(1, 31))
            .RuleFor(x => x.Info, (f, s) =>
            {
                var recordFaker = _StudentRegisterRecordFaker;

                return recordFaker.Generate();
            });


        private static Faker<Subject> _SubjectFaker => new Faker<Subject>()
            .RuleFor(x => x.Name, f => f.PickRandom(_SubjectNames));

        /// <summary> Max 13 </summary>
        private static Faker<Subject> _UniqueSubjectFaker
        {
            get
            {
                var randomInt = new Faker().Random.Int(0);
                return _SubjectFaker.
                    RuleFor(x => x.Name, f => _SubjectNames[(randomInt + f.IndexFaker) % _SubjectNames.Length]);
            }
        }

        private static Faker<Teacher> _TeacherFaker => new Faker<Teacher>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.SecondName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName());



        private static Faker<Room> _RoomFaker => new Faker<Room>()
            .RuleFor(x => x.Name, f => f.PickRandom(_RoomNames))
            .RuleFor(x => x.Number, f => f.Random.Int(1, 30))
            .RuleFor(x => x.Floor, f => f.Random.Int(0, 3));




        private static Faker<User> _UserFaker => new Faker<User>()
            .RuleFor(x => x.UserName, f => f.Internet.UserName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.EmailConfirmed, true);




        #region OrganizationalClasses

        public static async Task<OrganizationalClass> Class_3b_27Students(
            SchoolYear year,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 3,
                Distinction = "b"
            };

            int i = 1;
            orgClass.Students = _StudentFaker
                .RuleFor(x => x.SchoolYearId, _ => year.Id)
                .RuleFor(x => x.NumberInJournal, _ => i++)
                .Generate(27);

            orgClassRepo.Add(orgClass);
            await orgClassRepo.SaveAsync();

            return orgClass;
        }

        public static async Task<OrganizationalClass> Class_2a_Mechanics_15Students(
            SchoolYear year,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 2,
                Distinction = "a",
                Specialization = "Mechanics"
            };

            int i = 1;
            orgClass.Students = _StudentFaker
                .RuleFor(x => x.SchoolYearId, _ => year.Id)
                .RuleFor(x => x.NumberInJournal, _ => i++)
                .Generate(15);

            orgClassRepo.Add(orgClass);
            await orgClassRepo.SaveAsync();

            return orgClass;
        }

        public static async Task<OrganizationalClass> Class_1e_0Students(
            SchoolYear year,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 1,
                Distinction = "e",
            };

            orgClassRepo.Add(orgClass);
            await orgClassRepo.SaveAsync();

            return orgClass;
        }


        public static async Task<OrganizationalClass> Class_4f_0Students_RandomSchedule(
            SchoolYear year,
            IRepository<OrganizationalClass> orgClassRepo,
            IRepository<Teacher> teacherRepo,
            int? fromHour = null,
            int? toHour = null)
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 4,
                Distinction = "f"
            };

            var subjects = _UniqueSubjectFaker.Generate(5);

            var teachers = await _5Random_Teachers(teacherRepo, subjects);

            var rooms = _RoomFaker.Generate(10);

            for (int weekDay = 1; weekDay < 6; weekDay++)
            {
                int[] hours = RandomAmountOfRandomInts(fromHour ?? 6, toHour ?? 17);

                foreach (var hour in hours)
                {
                    var teacher = new Faker().PickRandom(teachers);

                    var subject = new Faker().PickRandom(teacher.SubjectOperations.MainIter);

                    var room = new Faker().PickRandom(rooms);

                    var lesson = new PeriodicLesson
                    {
                        SchoolYearId = year.Id,
                        LecturerId = teacher.Id,
                        SubjectId = subject.Id,
                        Room = room,
                        ParticipatingOrganizationalClass = orgClass,
                        CronPeriodicity = $"0 {hour} * * {weekDay}"
                    };

                    orgClass.Schedule.Add(lesson);
                }
            }

            orgClassRepo.Add(orgClass);
            await orgClassRepo.SaveAsync();

            return orgClass;
        }

        public static async Task<OrganizationalClass> Class_5f_0Students_RandomScheduleAddedSeparately(
            SchoolYear year,
            IRepository<OrganizationalClass> orgClassRepo,
            IRepository<Teacher> teacherRepo,
            IRepository<PeriodicLesson> lessonRepo,
            int? fromHour = null,
            int? toHour = null)
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 5,
                Distinction = "f"
            };
            orgClassRepo.Add(orgClass);
            await orgClassRepo.SaveAsync();

            var subjects = _UniqueSubjectFaker.Generate(5);

            var teachers = await _5Random_Teachers(teacherRepo, subjects);

            var rooms = _RoomFaker.Generate(10);

            for (int weekDay = 1; weekDay < 6; weekDay++)
            {
                int[] hours = RandomAmountOfRandomInts(fromHour ?? 6, toHour ?? 17);

                foreach (var hour in hours)
                {
                    var teacher = new Faker().PickRandom(teachers);

                    var subject = new Faker().PickRandom(teacher.SubjectOperations.MainIter);

                    var room = new Faker().PickRandom(rooms);

                    var lesson = new PeriodicLesson
                    {
                        SchoolYearId = year.Id,
                        LecturerId = teacher.Id,
                        SubjectId = subject.Id,
                        Room = room,
                        ParticipatingOrganizationalClass = orgClass,
                        CronPeriodicity = $"0 {hour} * * {weekDay}"
                    };

                    lessonRepo.Add(lesson);
                }
            }
            await lessonRepo.SaveAsync();

            return orgClass;
        }



        #endregion

        #region Students


        public static async Task<StudentRegisterRecord> StudentRegisterRecord(
            IRepository<StudentRegisterRecord> registerRepo,
            string? firstName = null,
            string? lastName = null,
            string? address = null)
        {
            var student = _StudentRegisterRecordFaker.Generate();

            if (firstName is not null) student.FirstName = firstName;
            if (lastName is not null) student.LastName = lastName;
            if (address is not null) student.Address = address;

            registerRepo.Add(student);
            await registerRepo.SaveAsync();

            return student;
        }

        public static async Task<Student> Student(
            SchoolYear year,
            IRepository<Student> studentRepo,
            string? firstName = null,
            string? lastName = null,
            string? address = null,
            long? organizationalClassId = null,
            int? numberInJournal = null)
        {
            var student = _StudentFaker.Generate();

            student.SchoolYearId = year.Id;
            if (firstName is not null) student.Info.FirstName = firstName;
            if (lastName is not null) student.Info.LastName = lastName;
            if (address is not null) student.Info.Address = address;
            if (organizationalClassId is not null) student.OrganizationalClassId = organizationalClassId.Value;
            if (numberInJournal is not null) student.NumberInJournal = numberInJournal.Value;

            studentRepo.Add(student);
            await studentRepo.SaveAsync();

            return student;
        }


        #endregion

        #region Subjects

        /// <param name="amount"> max. 13 </param>
        public static async Task<IEnumerable<Subject>> Subjects(
            IRepository<Subject> subjectRepo,
            int amount)
        {
            var subjects = _UniqueSubjectFaker.Generate(Math.Min(amount, 13));

            subjectRepo.AddRange(subjects);
            await subjectRepo.SaveAsync();

            return subjects;
        }

        private static IEnumerable<Subject>? _OnceCreatedSubjects;
        public static async Task<IEnumerable<Subject>> GetOrCreateUniqueSubjects(
            IRepository<Subject> subjectRepo)
        {
            if (_OnceCreatedSubjects is null
                || !_OnceCreatedSubjects.Any()
                || _OnceCreatedSubjects.Any(x => subjectRepo.GetEntry(x).State == Microsoft.EntityFrameworkCore.EntityState.Detached))
            {
                _OnceCreatedSubjects = await Subjects(subjectRepo, 10);
            }

            return _OnceCreatedSubjects;
        }

        #endregion

        #region Teachers

        public static async Task<Teacher> Teacher(
            IRepository<Teacher> teacherRepo,
            IList<Subject>? notSavedSubjects = null)
        {
            var teachers = await _XRandom_Teachers(teacherRepo, 1, notSavedSubjects);

            foreach (var teacher in teachers)
                return teacher;
            throw new Exception();
        }

        public static async Task<IEnumerable<Teacher>> _5Random_Teachers(
            IRepository<Teacher> teacherRepo,
            IList<Subject>? notSavedSubjects = null)
        {
            var teachers = await _XRandom_Teachers(teacherRepo, 5, notSavedSubjects);

            return teachers;
        }

        public static async Task<IEnumerable<Teacher>> _XRandom_Teachers(
            IRepository<Teacher> teacherRepo,
            int amount,
            IList<Subject>? notSavedSubjects = null)
        {
            var subjects = notSavedSubjects ?? _UniqueSubjectFaker.Generate(13);

            var teachers = new List<Teacher>();
            for (int i = 0; i < amount; i++)
            {
                var teacher = _TeacherFaker
                    .FinishWith((f, t) =>
                    {
                        var randomInts = RandomAmountOfRandomInts(0, subjects.Count - 1);

                        int lengthOr5 = Math.Min(5, randomInts.Length);

                        for (int j = 0; j < lengthOr5; j++)
                        {
                            if (!(j > lengthOr5 / 2))
                                t.SubjectOperations.AddNewlyCreatedMain(subjects[randomInts[j]]);
                            else
                                t.SubjectOperations.AddNewlyCreatedAdditional(subjects[randomInts[j]]);
                        }
                    })
                    .Generate();
                teachers.Add(teacher);
            }
            teacherRepo.AddRange(teachers);
            await teacherRepo.SaveAsync();

            return teachers;
        }

        #endregion

        #region Rooms


        public static async Task<Room> Room(
            IRepository<Room> roomRepo)
        {
            var room = _RoomFaker.Generate();

            roomRepo.Add(room);
            await roomRepo.SaveAsync();

            return room;
        }


        #endregion

        #region Users

        public static async Task<User> UserWithRandomStudent(
            IUserRepository userRepo)
        {
            var studentRecord = _StudentRegisterRecordFaker.Generate();

            var user = _UserFaker
                .RuleFor(x => x.UserName, f => f.Internet.UserName(studentRecord.FirstName, studentRecord.LastName))
                .RuleFor(x => x.Email, f => f.Internet.Email(studentRecord.FirstName, studentRecord.LastName))
                .Generate();

            user.Type = UserType.Student;
            user.Student = studentRecord;

            await userRepo.Manager.CreateAsync(user);

            return user;
        }

        public static async Task<IList<User>> UsersWithRandomStudents(
            IUserRepository userRepo,
            int amount)
        {
            var users = new List<User>();

            while (0 < amount--)
            {
                users.Add(await UserWithRandomStudent(userRepo));
            }

            return users;
        }

        public static async Task<User> UserWithRandomTeacher(
            IUserRepository userRepo,
            IRepository<Teacher> teacherRepo)
        {
            var teacher = await Teacher(teacherRepo);

            var user = _UserFaker
                .RuleFor(x => x.UserName, f => f.Internet.UserName(teacher.FirstName, teacher.LastName))
                .RuleFor(x => x.Email, f => f.Internet.Email(teacher.FirstName, teacher.LastName))
                .Generate();

            user.Type = UserType.Teacher;
            user.TeacherId = teacher.Id;

            await userRepo.Manager.CreateAsync(user);

            user.Teacher = teacher;
            return user;
        }

        public static async Task<IList<User>> UsersWithRandomTeachers(
            IUserRepository userRepo,
            IRepository<Teacher> teacherRepo,
            int amount)
        {
            var users = new List<User>();

            var teachers = await _XRandom_Teachers(teacherRepo, amount);

            foreach (var teacher in teachers)
            {
                var user = _UserFaker
               .RuleFor(x => x.UserName, f => f.Internet.UserName(teacher.FirstName, teacher.LastName))
               .RuleFor(x => x.Email, f => f.Internet.Email(teacher.FirstName, teacher.LastName))
               .Generate();

                user.Type = UserType.Teacher;
                user.TeacherId = teacher.Id;

                try
                {
                    await userRepo.Manager.CreateAsync(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                users.Add(user);
            }

            var ids = users.Select(x => x.Id);
            return await userRepo.AsQueryable().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        #endregion


        #region Periodic lessons and held lessons

        public static async Task<(
            OrganizationalClass class4f,
            OrganizationalClass class5f
            )> ScheduleOf_4f_5f_Classes_WithLessonEntities(
                SchoolYear year,
                IRepository<OrganizationalClass> orgClassRepo,
                IRepository<Teacher> teacherRepo,
                IRepository<PeriodicLesson> lessonRepo,
                int? fromHour = null,
                int? toHour = null)
        {
            // TODO: generate classes with students

            var class4f = await FakeData.Class_4f_0Students_RandomSchedule(year, orgClassRepo, teacherRepo, fromHour, toHour);
            var class5f = await FakeData.Class_5f_0Students_RandomScheduleAddedSeparately(year, orgClassRepo, teacherRepo, lessonRepo, fromHour, toHour);

            var periLessons = await lessonRepo.AsListAsync();
            var schoolYearId = year.Id;

            foreach (var periLesson in periLessons)
            {
                var pastLessonDates = periLesson.GetOccurrences(DateTime.Now.AddMonths(-12), DateTime.Now);

                var lessons = pastLessonDates.Select(x => new Lesson
                {
                    Date = x,
                    FromScheduleId = periLesson.Id,
                    SchoolYearId = schoolYearId,
                    Topic = "Some topic",
                    PresenceOfStudents = periLesson.ParticipatingOrganizationalClass!.Students.Select(x => new Presence
                    {
                        SchoolYearId = schoolYearId,
                        StudentId = x.Id,
                        Status = PresenceStatus.Present
                    }).ToList()
                }).ToList();

                periLesson.TakenLessons = lessons;
                lessonRepo.Update(periLesson);
            }

            await lessonRepo.SaveAsync();

            return (class4f, class5f);
        }

        #endregion


        public static int[] RandomAmountOfRandomInts(int from, int to)
        {
            var faker = new Faker();
            var ints = new List<int>();
            var amount = faker.Random.Int((to - from) / 2, to - from - 1);

            for (; amount > 0; amount--)
            {
                var random = faker.Random.Int(from, to);

                if (ints.Contains(random))
                {
                    amount++; continue;
                }
                ints.Add(random);
            }

            return ints.ToArray();
        }
    }
}
