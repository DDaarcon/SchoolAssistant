using NUnit.Framework;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.ScheduleDisplay;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ScheduleDisplay
{
    public class TeacherScheduleServiceTests : BaseDbEntitiesTests
    {
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepository<Student> _studentRepo = null!;
        private IRepositoryBySchoolYear<PeriodicLesson> _lessonRepo = null!;

        private ITeacherScheduleService _teacherScheduleSvc = null!;

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
        }

        protected override void SetupServices()
        {
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);
            _studentRepo = new Repository<Student>(_Context, null);

            _lessonRepo = new RepositoryBySchoolYear<PeriodicLesson>(_Context, null, _schoolYearRepo);

            _teacherScheduleSvc = new TeacherScheduleService(_schoolYearRepo, _teacherRepo, _lessonRepo);
        }

        [Test]
        public async Task Should_fetch_teachers_lessons()
        {
            var lessonsDb = await _lessonRepo.AsListByYear.ByCurrentAsync();
            var teacherId = lessonsDb.First().LecturerId;

            var res = await _teacherScheduleSvc.GetModelForCurrentYearAsync(teacherId);

            Assert.IsNotNull(res);
            Assert.IsNotEmpty(res!);

            Assert.IsTrue(res!.SelectMany(x => x.lessons)
                .All(x => lessonsDb.Any(d =>
                    x.id == d.Id
                    && x.customDuration == d.CustomDuration
                    && x.time.hour == d.GetTime()!.Value.Hour
                    && x.time.minutes == d.GetTime()!.Value.Minute
                    && x.lecturer.id == d.LecturerId
                    && x.subject.id == d.SubjectId
                    && x.room.id == d.RoomId
                    && x.orgClass?.id == d.ParticipatingOrganizationalClassId
                    && x.subjClass?.id == d.ParticipatingSubjectClassId)));
        }
    }
}
