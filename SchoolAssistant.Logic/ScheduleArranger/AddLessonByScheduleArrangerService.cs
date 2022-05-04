using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IAddLessonByScheduleArrangerService
    {
        Task<AddLessonResponseJson> AddToClass(AddLessonRequestJson model);
    }

    [Injectable]
    public class AddLessonByScheduleArrangerService : IAddLessonByScheduleArrangerService
    {
        private readonly IAppConfigRepository _configRepo;
        private readonly IRepository<OrganizationalClass> _orgClassRepo;
        private readonly IRepository<Subject> _subjectRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Room> _roomRepo;
        private readonly IRepositoryBySchoolYear<PeriodicLesson> _lessonsRepo;

        private OrganizationalClass _orgClass = null!;
        private PeriodicLesson _entity = null!;
        private AddLessonRequestJson _model = null!;
        private AddLessonResponseJson _response = null!;

        public AddLessonByScheduleArrangerService(
            IAppConfigRepository configRepo,
            IRepository<OrganizationalClass> orgClassRepo,
            IRepository<Subject> subjectRepo,
            IRepository<Teacher> teacherRepo,
            IRepository<Room> roomRepo,
            IRepositoryBySchoolYear<PeriodicLesson> lessonsRepo)
        {
            _configRepo = configRepo;
            _orgClassRepo = orgClassRepo;
            _subjectRepo = subjectRepo;
            _teacherRepo = teacherRepo;
            _roomRepo = roomRepo;
            _lessonsRepo = lessonsRepo;
        }


        public async Task<AddLessonResponseJson> AddToClass(AddLessonRequestJson model)
        {
            _model = model;
            _response = new AddLessonResponseJson();

            if (!await ValidateAsync())
                return _response;

            await CreateAsync();

            await PrepareDataInResponse();

            return _response;
        }

        private async Task<bool> ValidateAsync()
        {
            if (_model is null)
                return ValidationFail("Błąd! Brakuje modelu danych");

            if (_model.time is null)
                return ValidationFail("Błąd! Brakuje danych na temat godziny rozpoczęcia zajęć");

            if (!Enum.IsDefined(_model.day))
                return ValidationFail("Błąd! Podano niezdefiniowany dzień");

            if (!await ValidateTime())
                return ValidationFail("Wybrano nieodpowiednią godzinę dla zajęć");

            if (!await _orgClassRepo.ExistsAsync(_model.classId))
                return ValidationFail("Błąd! Klasa, do której dodawane są zajęcia, nie istnieje");

            if (!await _subjectRepo.ExistsAsync(_model.subjectId))
                return ValidationFail("Błąd! Przedmiot, którego mają dotyczyć zajęcia, nie istnieje");

            if (!await _teacherRepo.ExistsAsync(_model.lecturerId))
                return ValidationFail("Błąd! Nauczyciel, prowadzący zajęcia, nie istnieje");

            if (!await _roomRepo.ExistsAsync(_model.roomId))
                return ValidationFail("Błąd! Sala, w której odbywać się mają zajęcia, nie istnieje");

            if (!await ValidateOverlappingWithOther())
                return ValidationFail("Zajęcia kolidują z innymi");

            return true;
        }

        private async Task<bool> ValidateTime()
        {
            var minHour = await _configRepo.ScheduleStartHour.GetAsync() ?? 0;
            var maxHour = await _configRepo.ScheduleEndhour.GetAsync() ?? 24;
            var duration = _model.customDuration ?? await _configRepo.DefaultLessonDuration.GetAsync() ?? 45;
            var lessonEndHour = _model.time.hour + (_model.time.minutes + duration) / 60;

            return _model.time.minutes >= 0 && _model.time.minutes < 60
                && _model.time.hour >= minHour
                && (lessonEndHour < maxHour
                    || (lessonEndHour == maxHour
                        && (_model.time.minutes + duration) % 60 == 0));
        }

        private async Task<bool> ValidateOverlappingWithOther()
        {
            _orgClass = (await _orgClassRepo.GetByIdAsync(_model.classId))!;

            var lessons = await _lessonsRepo.AsQueryableByYear.ByYearOf(_orgClass)
                .Where(x =>
                    x.ParticipatingOrganizationalClassId == _model.classId
                    || x.LecturerId == _model.lecturerId
                    || x.SubjectId == _model.subjectId
                    || x.RoomId == _model.roomId)
                .ToListAsync();
            var lessonsThatDay = lessons.Where(x => x.GetDayOfWeek() == _model.day);

            var defaultDuration = await _configRepo.DefaultLessonDuration.GetAsync() ?? 45;

            foreach (var lesson in lessonsThatDay)
            {
                var lessonStart = lesson.GetTime()!.Value;
                var lessonDur = lesson.CustomDuration ?? defaultDuration;

                var newStart = new TimeOnly(_model.time.hour, _model.time.minutes);
                var newDur = _model.customDuration ?? defaultDuration;
                if (TimeHelper.AreOverlapping(lessonStart, lessonDur, newStart, newDur))
                    return false;
            }

            return true;
        }


        private bool ValidationFail(string errorMsg)
        {
            _response.message = errorMsg;
            return false;
        }

        private async Task CreateAsync()
        {
            _entity = new PeriodicLesson
            {
                SchoolYearId = _orgClass.SchoolYearId,
                CronPeriodicity = CronExpressionsHelper.Weekly(_model.time.hour, _model.time.minutes, _model.day),
                CustomDuration = _model.customDuration,
                LecturerId = _model.lecturerId,
                ParticipatingOrganizationalClass = _orgClass,
                RoomId = _model.roomId,
                SubjectId = _model.subjectId
            };

            _lessonsRepo.Add(_entity);
            await _lessonsRepo.SaveAsync();
        }

        private async Task PrepareDataInResponse()
        {
            _response.lesson = new PeriodicLessonTimetableEntryJson
            {
                id = _entity.Id,
                customDuration = _entity.CustomDuration,
                time = new TimeJson(_entity.GetTime()!.Value),
                lecturer = new IdNameJson
                {
                    id = _entity.LecturerId,
                    name = (await _teacherRepo.GetByIdAsync(_entity.LecturerId))!.GetShortenedName()
                },
                subject = new IdNameJson
                {
                    id = _entity.SubjectId,
                    name = (await _subjectRepo.GetByIdAsync(_entity.SubjectId))!.Name
                },
                room = new IdNameJson
                {
                    id = _entity.RoomId,
                    name = (await _roomRepo.GetByIdAsync(_entity.RoomId))!.DisplayName
                }
            };
        }
    }
}
