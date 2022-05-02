using NUnit.Framework;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.ScheduleArranger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ScheduleArranger
{
    public class AddLessonTests : BaseDbEntitiesTests
    {
        private IAddLessonService _addLessonService = null!;

        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepository<Room> _roomRepo = null!;


        private OrganizationalClass _classWithNoSchedule = null!;
        private IEnumerable<Teacher> _teachers = null!;
        private IList<Room> _rooms = null!;

        private Teacher _Teacher => _teachers.First();
        private Room _Room => _rooms.First();

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<PeriodicLesson>();
        }

        protected override async Task SetupDataForEveryTestAsync()
        {
            _classWithNoSchedule = await FakeData.Class_1e_0Students(await _Year, _orgClassRepo);
            _teachers = await FakeData._5Random_Teachers(_teacherRepo);
            _rooms = new List<Room>();
            for (int i = 0; i < 10; i++)
                _rooms.Add(await FakeData.Room(_roomRepo));
        }

        protected override void SetupServices()
        {
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);
            _roomRepo = new Repository<Room>(_Context, null);

            _addLessonService = new AddLessonService();
        }



        [Test]
        public async Task Should_add_lesson()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                }
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseSuccess(res);

            Assert.AreEqual(res!.lesson!.time, model.time);
            Assert.IsNull(res!.lesson!.customDuration);
            Assert.AreEqual(res!.lesson!.lecturer.name, _Teacher.GetShortenedName());
            Assert.AreEqual(res!.lesson!.subject.name, subject.Name);
            Assert.AreEqual(res!.lesson!.room.name, _Room.DisplayName);
            Assert.AreEqual(res!.lesson!.lecturer.id, model.lecturerId);
            Assert.AreEqual(res!.lesson!.subject.id, model.subjectId);
            Assert.AreEqual(res!.lesson!.room.id, model.roomId);
        }
    }
}
