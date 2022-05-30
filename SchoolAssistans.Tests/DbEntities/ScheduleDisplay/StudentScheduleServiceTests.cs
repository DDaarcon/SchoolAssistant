using NUnit.Framework;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.Schedule;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ScheduleDisplay
{
    public class StudentScheduleServiceTests : BaseDbEntitiesTests
    {
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepository<Student> _studentRepo = null!;

        private IStudentScheduleService _studentScheduleSvc = null!;

        private OrganizationalClass _orgClass1 = null!;

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<PeriodicLesson>();
            await TestDatabase.ClearDataAsync<Teacher>();
            await TestDatabase.ClearDataAsync<Subject>();
            await TestDatabase.ClearDataAsync<Room>();
            await TestDatabase.ClearDataAsync<Student>();
            await TestDatabase.ClearDataAsync<OrganizationalClass>();
        }

        protected override async Task SetupDataForEveryTestAsync()
        {
            _orgClass1 = await FakeData.Class_4f_0Students_RandomSchedule(await _Year, _orgClassRepo, _teacherRepo);

            _orgClass1.Students.Add(await FakeData.Student(await _Year, _studentRepo));

            await _orgClassRepo.SaveAsync();
        }

        protected override void SetupServices()
        {
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);
            _studentRepo = new Repository<Student>(_Context, null);

            _studentScheduleSvc = new StudentScheduleService(_studentRepo);
        }

        [Test]
        public void Should_fetch_class_lessons_for_schedule_by_student()
        {
            var student = _orgClass1.Students.First();

            var lessons = _studentScheduleSvc.GetModel(student);

            Assert.IsNotNull(lessons);
            Assert.IsNotEmpty(lessons!);

            Assert.IsTrue(lessons!.SelectMany(x => x.lessons)
                .All(x => _orgClass1.Schedule.Any(d =>
                    x.id == d.Id
                    && x.time.hour == d.GetTime()!.Value.Hour
                    && x.time.minutes == d.GetTime()!.Value.Minute
                    && x.customDuration == d.CustomDuration
                    && x.subject.id == d.Subject.Id
                    && x.lecturer.id == d.Lecturer.Id
                    && x.room.id == d.Room.Id)));
        }
    }
}
