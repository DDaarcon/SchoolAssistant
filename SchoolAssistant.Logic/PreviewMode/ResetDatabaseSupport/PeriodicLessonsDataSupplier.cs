using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface IPeriodicLessonsDataSupplier
    {
        IEnumerable<PeriodicLesson> All { get; }

        void InitializeData();
    }

    [Injectable]
    public class PeriodicLessonsDataSupplier : IPeriodicLessonsDataSupplier
    {
        private readonly IOrganizationalClassDataSupplier _orgClassDataSupplier;
        private readonly ITeachersDataSupplier _teachersDataSupplier;
        private readonly IRoomsDataSupplier _roomsDataSupplier;
        private readonly ISubjectsDataSupplier _subjectsDataSupplier;

        public PeriodicLessonsDataSupplier(
            IOrganizationalClassDataSupplier orgClassDataSupplier,
            ITeachersDataSupplier teachersDataSupplier,
            ISubjectsDataSupplier subjectsDataSupplier,
            IRoomsDataSupplier roomsDataSupplier)
        {
            _orgClassDataSupplier = orgClassDataSupplier;
            _teachersDataSupplier = teachersDataSupplier;
            _subjectsDataSupplier = subjectsDataSupplier;
            _roomsDataSupplier = roomsDataSupplier;
        }

        public IEnumerable<PeriodicLesson> All { get; private set; }


        public void InitializeData()
        {
            var all = new List<PeriodicLesson>();

            #region Class 1a
            var class1a = _orgClassDataSupplier.Class1a;

            all.AddRange(Create(class1a,
                _teachersDataSupplier.SampleTeacher,
                _subjectsDataSupplier.Math.Id,
                _roomsDataSupplier.Room1.Id,
                new LessonTime(DayOfWeek.Monday, new TimeOnly(7, 0)),
                new LessonTime(DayOfWeek.Monday, new TimeOnly(7, 50)),
                new LessonTime(DayOfWeek.Tuesday, new TimeOnly(7, 0))));
            all.AddRange(Create(class1a,
                _teachersDataSupplier.PolishHistory1,
                _subjectsDataSupplier.Polish.Id,
                _roomsDataSupplier.Room2.Id,
                new LessonTime(DayOfWeek.Tuesday, new TimeOnly(7, 50)),
                new LessonTime(DayOfWeek.Monday, new TimeOnly(8, 40)),
                new LessonTime(DayOfWeek.Friday, new TimeOnly(7, 50))));
            all.AddRange(Create(class1a,
                _teachersDataSupplier.BiologyChemistry1,
                _subjectsDataSupplier.Biology.Id,
                _roomsDataSupplier.Room3.Id,
                new LessonTime(DayOfWeek.Monday, new TimeOnly(9, 35)),
                new LessonTime(DayOfWeek.Wednesday, new TimeOnly(7, 50))));
            all.AddRange(Create(class1a,
                _teachersDataSupplier.English1,
                _subjectsDataSupplier.English.Id,
                _roomsDataSupplier.Room1.Id,
                new LessonTime(DayOfWeek.Thursday, new TimeOnly(8, 40)),
                new LessonTime(DayOfWeek.Tuesday, new TimeOnly(8, 40))));
            all.AddRange(Create(class1a,
                _teachersDataSupplier.PolishHistory1,
                _subjectsDataSupplier.History.Id,
                _roomsDataSupplier.Room2.Id,
                new LessonTime(DayOfWeek.Wednesday, new TimeOnly(9, 35)),
                new LessonTime(DayOfWeek.Wednesday, new TimeOnly(8, 40))));
            #endregion

            #region Class 1b
            all.AddRange(Create(class1a,
                _teachersDataSupplier.Math2,
                _subjectsDataSupplier.Math.Id,
                _roomsDataSupplier.Room1.Id,
                new LessonTime(DayOfWeek.Monday, new TimeOnly(8, 40)),
                new LessonTime(DayOfWeek.Tuesday, new TimeOnly(7, 50)),
                new LessonTime(DayOfWeek.Wednesday, new TimeOnly(8, 40)),
                new LessonTime(DayOfWeek.Thursday, new TimeOnly(9, 30))));
            all.AddRange(Create(class1a,
                _teachersDataSupplier.Physics2,
                _subjectsDataSupplier.Physics.Id,
                _roomsDataSupplier.Room2.Id,
                new LessonTime(DayOfWeek.Monday, new TimeOnly(7, 50)),
                new LessonTime(DayOfWeek.Wednesday, new TimeOnly(7, 0)),
                new LessonTime(DayOfWeek.Thursday, new TimeOnly(10, 20))));
            all.AddRange(Create(class1a,
                _teachersDataSupplier.English1,
                _subjectsDataSupplier.English.Id,
                _roomsDataSupplier.Room4.Id,
                new LessonTime(DayOfWeek.Tuesday, new TimeOnly(7, 0)),
                new LessonTime(DayOfWeek.Thursday, new TimeOnly(7, 50)),
                new LessonTime(DayOfWeek.Friday, new TimeOnly(8, 40))));
            #endregion
            All = all;
        }

        private IEnumerable<PeriodicLesson> Create(
            OrganizationalClass orgClass,
            Teacher lecturer,
            long? subjectId,
            long roomId,
            params LessonTime[] times)
        {
            return times.Select(x => new PeriodicLesson
            {
                CronPeriodicity = CronExpressionsHelper.Weekly(x.Time.Hour, x.Time.Minute, x.DayOfWeek),
                LecturerId = lecturer.Id,
                ParticipatingOrganizationalClassId = orgClass.Id,
                SubjectId = subjectId ?? lecturer.MainSubjects.First().Subject.Id,
                SchoolYearId = orgClass.SchoolYear.Id,
                RoomId = roomId
            });
        }

        private record LessonTime(
            DayOfWeek DayOfWeek,
            TimeOnly Time);
    }
}
