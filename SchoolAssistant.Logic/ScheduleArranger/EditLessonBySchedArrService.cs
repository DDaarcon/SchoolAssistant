using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using SchoolAssistant.Logic.General.PeriodicLessons;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IEditLessonBySchedArrService
    {
        Task<ResponseJson> EditAsync(LessonEditModelJson model);
    }

    [Injectable]
    public class EditLessonBySchedArrService : IEditLessonBySchedArrService
    {
        private readonly IRepository<OrganizationalClass> _orgClassRepo;
        private readonly IRepository<Subject> _subjectRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Room> _roomRepo;
        private readonly IRepository<PeriodicLesson> _lessonRepo;
        private readonly IValidatePeriodicLessonJsonsService _validateModelsSvc;

        private LessonEditModelJson? _model;
        private PeriodicLesson? _entity;
        private ResponseJson? _response;

        public EditLessonBySchedArrService(
            IRepository<OrganizationalClass> orgClassRepo,
            IRepository<Subject> subjectRepo,
            IRepository<Teacher> teacherRepo,
            IRepository<Room> roomRepo,
            IRepository<PeriodicLesson> lessonRepo,
            IValidatePeriodicLessonJsonsService validateModelsSvc)
        {
            _orgClassRepo = orgClassRepo;
            _subjectRepo = subjectRepo;
            _teacherRepo = teacherRepo;
            _roomRepo = roomRepo;
            _lessonRepo = lessonRepo;
            _validateModelsSvc = validateModelsSvc;
        }

        public async Task<ResponseJson> EditAsync(LessonEditModelJson model)
        {
            _model = model;
            _response = new ResponseJson();

            if (!await ValidateAsync())
                return _response;

            await UpdateAsync();

            return _response;
        }

        private async Task<bool> ValidateAsync()
        {
            if (_model is null)
                return ValidationFail("Błąd! Brakuje modelu danych");

            if (!_model.id.HasValue)
                return ValidationFail("Błąd! Brakuje id zajęć");

            if (!await _lessonRepo.ExistsAsync(_model.id.Value))
                return ValidationFail("Błąd! Nie odnaleziono modyfikowanej lekcji");

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
            _response!.message = errorMsg;
            return false;
        }

        private async Task UpdateAsync()
        {
            var orgClass = (await _orgClassRepo.GetByIdAsync(_model!.classId))!;

            _entity = (await _lessonRepo.GetByIdAsync(_model.id!.Value))!;

            _entity.SchoolYearId = orgClass.SchoolYearId;
            _entity.CronPeriodicity = CronExpressionsHelper.Weekly(_model.time.hour, _model.time.minutes, _model.day);
            _entity.CustomDuration = _model.customDuration;
            _entity.LecturerId = _model.lecturerId;
            _entity.ParticipatingOrganizationalClass = orgClass;
            _entity.RoomId = _model.roomId;
            _entity.SubjectId = _model.subjectId;

            await _lessonRepo.SaveAsync();
        }
    }
}
