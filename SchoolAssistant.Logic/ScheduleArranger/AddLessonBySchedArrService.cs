using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Logic.General.PeriodicLessons;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IAddLessonBySchedArrService
    {
        Task<AddLessonResponseJson> AddToClassAsync(AddLessonRequestJson model);
    }

    [Injectable]
    public class AddLessonBySchedArrService : IAddLessonBySchedArrService
    {
        private readonly IAppConfigRepository _configRepo;
        private readonly IRepository<OrganizationalClass> _orgClassRepo;
        private readonly IRepository<Subject> _subjectRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Room> _roomRepo;
        private readonly IRepositoryBySchoolYear<PeriodicLesson> _lessonsRepo;
        private readonly IValidateJsonModelsService _validateModelsSvc;

        private OrganizationalClass _orgClass = null!;
        private PeriodicLesson _entity = null!;
        private AddLessonRequestJson _model = null!;
        private AddLessonResponseJson _response = null!;

        public AddLessonBySchedArrService(
            IAppConfigRepository configRepo,
            IRepository<OrganizationalClass> orgClassRepo,
            IRepository<Subject> subjectRepo,
            IRepository<Teacher> teacherRepo,
            IRepository<Room> roomRepo,
            IRepositoryBySchoolYear<PeriodicLesson> lessonsRepo,
            IValidateJsonModelsService validateModelsSvc)
        {
            _configRepo = configRepo;
            _orgClassRepo = orgClassRepo;
            _subjectRepo = subjectRepo;
            _teacherRepo = teacherRepo;
            _roomRepo = roomRepo;
            _lessonsRepo = lessonsRepo;
            _validateModelsSvc = validateModelsSvc;
        }


        public async Task<AddLessonResponseJson> AddToClassAsync(AddLessonRequestJson model)
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

            if (!await _validateModelsSvc.ValidateTime(_model.time, _model.customDuration))
                return ValidationFail("Wybrano nieodpowiednią godzinę dla zajęć");

            if (!await _orgClassRepo.ExistsAsync(_model.classId))
                return ValidationFail("Błąd! Klasa, do której dodawane są zajęcia, nie istnieje");

            if (!await _subjectRepo.ExistsAsync(_model.subjectId))
                return ValidationFail("Błąd! Przedmiot, którego mają dotyczyć zajęcia, nie istnieje");

            if (!await _teacherRepo.ExistsAsync(_model.lecturerId))
                return ValidationFail("Błąd! Nauczyciel, prowadzący zajęcia, nie istnieje");

            if (!await _roomRepo.ExistsAsync(_model.roomId))
                return ValidationFail("Błąd! Sala, w której odbywać się mają zajęcia, nie istnieje");

            if (!await _validateModelsSvc.ValidateOverlapping(_model))
                return ValidationFail("Zajęcia kolidują z innymi");

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
            _response.lesson = new ScheduleLessonTimetableEntryJson
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
