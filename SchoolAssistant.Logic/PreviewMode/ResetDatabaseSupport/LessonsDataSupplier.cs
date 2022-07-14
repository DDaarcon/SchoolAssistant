using Bogus;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface ILessonsDataSupplier
    {
        IEnumerable<Lesson> All { get; }

        Task InitializeDataAsync();
    }

    [Injectable]
    public class LessonsDataSupplier : ILessonsDataSupplier
    {
        private readonly IPeriodicLessonsDataSupplier _perioLessDataSupplier;
        private readonly ISchoolYearDataSupplier _schoolYearDataSupplier;
        private readonly IRepository<OrganizationalClass> _orgClassRepo;

        public LessonsDataSupplier(
            IPeriodicLessonsDataSupplier perioLessDataSupplier,
            ISchoolYearDataSupplier schoolYearDataSupplier, 
            IRepository<OrganizationalClass> orgClassRepo)
        {
            _perioLessDataSupplier = perioLessDataSupplier;
            _schoolYearDataSupplier = schoolYearDataSupplier;
            _orgClassRepo = orgClassRepo;
        }

        public IEnumerable<Lesson> All { get; private set; } = null!;

        private Faker _faker = new Faker("pl");

        public async Task InitializeDataAsync()
        {
            var yearStart = DateTime.SpecifyKind(new DateTime(_schoolYearDataSupplier.Current.Year, 9, 1), DateTimeKind.Utc);
            var all = new List<Lesson>();

            var groupedByClass = _perioLessDataSupplier.All
                .GroupBy(x => x.ParticipatingOrganizationalClassId)
                .OrderBy(x => x.Key);


            foreach (var classAndScheduledLesson in groupedByClass)
            {
                OrganizationalClass orgClass = (await _orgClassRepo.GetByIdAsync(classAndScheduledLesson.Key ?? 0).ConfigureAwait(false))!;

                foreach (var periodicLesson in classAndScheduledLesson)
                {

                    var occurrences = periodicLesson.GetOccurrences(yearStart, DateTime.UtcNow);

                    var lessons = occurrences.Select(x => new Lesson
                    {
                        Date = x,
                        FromScheduleId = periodicLesson.Id,
                        SchoolYearId = periodicLesson.SchoolYear.Id,
                        Topic = _faker.Lorem.Text(),
                        PresenceOfStudents = orgClass.Students.Select(x => new Presence
                        {
                            SchoolYearId = periodicLesson.SchoolYear.Id,
                            StudentId = x.Id,
                            Status = GetRandomPresenceStatus()
                        }).ToList()
                    });

                    all.AddRange(lessons);
                }

            }

            All = all;
        }

        private PresenceStatus GetRandomPresenceStatus()
        {
            int num = _faker.Random.Int(0, 100);

            if (num < 80)
                return PresenceStatus.Present;
            if (num < 90)
                return PresenceStatus.Absent;

            return PresenceStatus.Late;
        }
    }
}
