using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Logic.ScheduleArranger;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ScheduleArranger
{
    public class FetchOtherLessonsTests : BaseDbEntitiesTests
    {
        IRepositoryBySchoolYear<PeriodicLesson> _lessonRepo = null!;
        IRepository<OrganizationalClass> _orgClassRepo = null!;
        IRepository<Teacher> _teacherRepo = null!;

        IFetchOtherLessonsForScheduleArrangerService _fetchLessonsService = null!;

        OrganizationalClass _orgClassWithSchedule = null!;
        OrganizationalClass _orgClass = null!;

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<PeriodicLesson>();
            await TestDatabase.ClearDataAsync<Teacher>();
            await TestDatabase.ClearDataAsync<OrganizationalClass>();
            await TestDatabase.ClearDataAsync<Room>();
        }

        protected override async Task SetupDataForEveryTestAsync()
        {
            _orgClassWithSchedule = await FakeData.Class_4f_0Students_RandomSchedule(await _Year, _orgClassRepo, _teacherRepo);
            _orgClass = await FakeData.Class_1e_0Students(await _Year, _orgClassRepo);
        }

        protected override void SetupServices()
        {
            _lessonRepo = new RepositoryBySchoolYear<PeriodicLesson>(_Context, null, _schoolYearRepo);
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);

            _fetchLessonsService = new FetchOtherLessonsForScheduleArrangerService(_lessonRepo, _orgClassRepo);
        }


        [Test]
        public async Task Should_fetch_lessons()
        {
            var teacher = await _teacherRepo.AsQueryable()
                .Where(x => x.Schedule.Any()).FirstAsync();

            var room = _orgClassWithSchedule.Schedule.First().Room;

            var res = await _fetchLessonsService.ForAsync(_orgClass.Id, teacher.Id, room.Id);

            Assert.IsNotNull(res);

            Assert.IsNotNull(res!.teacher);
            Assert.IsNotNull(res.room);

            Assert.AreEqual(_orgClassWithSchedule.Schedule.Where(x => x.LecturerId == teacher.Id).Count(), res.teacher!.Length);
            Assert.AreEqual(_orgClassWithSchedule.Schedule.Where(x => x.RoomId == room.Id).Count(), res.room!.Length);
        }

        [Test]
        public async Task Should_not_fetch_if_only_lessons_are_in_this_class()
        {
            var teacher = await _teacherRepo.AsQueryable()
                .Where(x => x.Schedule.Any()).FirstAsync();

            var room = _orgClassWithSchedule.Schedule.First().Room;

            var res = await _fetchLessonsService.ForAsync(_orgClassWithSchedule.Id, teacher.Id, room.Id);

            Assert.IsNotNull(res);

            Assert.IsNotNull(res!.teacher);
            Assert.IsNotNull(res.room);

            Assert.IsEmpty(res.teacher!);
            Assert.IsEmpty(res.room!);
        }

        [Test]
        public async Task Should_fetch_lessons_only_teacher()
        {
            var teacher = await _teacherRepo.AsQueryable()
                .Where(x => x.Schedule.Any()).FirstAsync();

            var res = await _fetchLessonsService.ForAsync(_orgClass.Id, teacher.Id, null);

            Assert.IsNotNull(res);

            Assert.IsNotEmpty(res!.teacher!);
            Assert.IsEmpty(res.room!);

            Assert.AreEqual(_orgClassWithSchedule.Schedule.Where(x => x.LecturerId == teacher.Id).Count(), res.teacher!.Length);
        }

        [Test]
        public async Task Should_fetch_lessons_only_room()
        {
            var room = _orgClassWithSchedule.Schedule.First().Room;

            var res = await _fetchLessonsService.ForAsync(_orgClass.Id, null, room.Id);

            Assert.IsNotNull(res);

            Assert.IsNotEmpty(res!.room!);
            Assert.IsEmpty(res!.teacher!);

            Assert.AreEqual(_orgClassWithSchedule.Schedule.Where(x => x.RoomId == room.Id).Count(), res.room!.Length);
        }


        #region Fails



        [Test]
        public async Task Should_fail_invalid_class_id()
        {
            var teacher = await _teacherRepo.AsQueryable()
                .Where(x => x.Schedule.Any()).FirstAsync();

            var room = _orgClassWithSchedule.Schedule.First().Room;

            var res = await _fetchLessonsService.ForAsync(9999, teacher.Id, room.Id);

            Assert.IsNull(res);
        }

        [Test]
        public async Task Should_fail_invalid_teacher_id()
        {
            var teacher = await _teacherRepo.AsQueryable()
                .Where(x => x.Schedule.Any()).FirstAsync();

            var room = _orgClassWithSchedule.Schedule.First().Room;

            var res = await _fetchLessonsService.ForAsync(_orgClass.Id, 9999, room.Id);

            Assert.IsNotNull(res);

            Assert.IsNotEmpty(res!.room!);
            Assert.IsEmpty(res.teacher!);
        }

        [Test]
        public async Task Should_fail_invalid_room_id()
        {
            var teacher = await _teacherRepo.AsQueryable()
                .Where(x => x.Schedule.Any()).FirstAsync();

            var room = _orgClassWithSchedule.Schedule.First().Room;

            var res = await _fetchLessonsService.ForAsync(_orgClass.Id, teacher.Id, 9999);

            Assert.IsNotNull(res);

            Assert.IsNotEmpty(res!.teacher!);
            Assert.IsEmpty(res.room!);
        }


        #endregion
    }
}
