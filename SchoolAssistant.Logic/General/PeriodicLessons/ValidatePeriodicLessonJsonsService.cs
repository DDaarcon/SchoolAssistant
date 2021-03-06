using AppConfigurationEFCore;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.General.PeriodicLessons
{
    public interface IValidatePeriodicLessonJsonsService
    {
        Task<bool> ValidateOverlapping(IValidateLessonJson model);
        Task<bool> ValidateTime(TimeJson time, int? customDuration);
    }

    [Injectable]
    public class ValidatePeriodicLessonJsonsService : IValidatePeriodicLessonJsonsService
    {
        private readonly IAppConfiguration<AppConfigRecords> _configRepo;
        private readonly IRepository<OrganizationalClass> _orgClassRepo;
        private readonly IRepositoryBySchoolYear<PeriodicLesson> _lessonRepo;

        public ValidatePeriodicLessonJsonsService(
            IAppConfiguration<AppConfigRecords> configRepo,
            IRepository<OrganizationalClass> orgClassRepo,
            IRepositoryBySchoolYear<PeriodicLesson> lessonRepo)
        {
            _configRepo = configRepo;
            _orgClassRepo = orgClassRepo;
            _lessonRepo = lessonRepo;
        }


        public async Task<bool> ValidateTime(TimeJson time, int? customDuration)
        {
            var minHour = await _configRepo.Records.ScheduleStartHour.GetAsync() ?? 0;
            var maxHour = await _configRepo.Records.ScheduleEndhour.GetAsync() ?? 24;
            var duration = customDuration ?? await _configRepo.Records.DefaultLessonDuration.GetAsync() ?? 45;
            var lessonEndHour = time.hour + (time.minutes + duration) / 60;

            return time.minutes >= 0 && time.minutes < 60
                && time.hour >= minHour
                && (lessonEndHour < maxHour
                    || (lessonEndHour == maxHour
                        && (time.minutes + duration) % 60 == 0));
        }

        public async Task<bool> ValidateOverlapping(IValidateLessonJson model)
        {
            var orgClass = (await _orgClassRepo.GetByIdAsync(model.classId))!;
            if (orgClass is null)
                return false;

            var lessons = await _lessonRepo.AsQueryableByYear.ByYearOf(orgClass)
                .Where(x =>
                    x.ParticipatingOrganizationalClassId == model.classId
                    || x.LecturerId == model.lecturerId
                    || x.RoomId == model.roomId)
                .Where(x => x.Id != model.id)
                .ToListAsync();
            var lessonsThatDay = lessons.Where(x => x.GetDayOfWeek() == model.day);

            var defaultDuration = await _configRepo.Records.DefaultLessonDuration.GetAsync() ?? 45;

            foreach (var lesson in lessonsThatDay)
            {
                var lessonStart = lesson.GetTime()!.Value;
                var lessonDur = lesson.CustomDuration ?? defaultDuration;

                var newStart = new TimeOnly(model.time.hour, model.time.minutes);
                var newDur = model.customDuration ?? defaultDuration;
                if (TimeHelper.AreOverlapping(lessonStart, lessonDur, newStart, newDur))
                    return false;
            }

            return true;
        }
    }
}
