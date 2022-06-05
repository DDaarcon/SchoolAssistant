using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;

namespace SchoolAssistant.Web.Pages.ConductingClasses
{
    public static class SpawnScheduledLessons
    {
        public static ScheduledLessonListItemModel[] _6LessonsFromNow => Enumerable.Range(0, 6)
            .Select(idx => new ScheduledLessonListItemModel
            {
                ClassName = RandomOf(new[] { "1e", "3d" })!,
                Duration = 45,
                StartTime = DateTime.Now.AddMinutes(50 * idx),
                SubjectName = RandomOf(new[] { "Polski", "Matma", "Ang" })!
            })
            .ToArray();



        private static T? RandomOf<T>(T[] options, params int[] probab)
        {
            if (options == null || options.Length == 0) return default;

            int randMax = options.Length - probab.Length + probab.Sum() - 1;

            int rand = new Random().Next(0, randMax);

            for (int i = 0; i < options.Length; i++)
            {
                if (rand == 0) return options[i];
                if (probab.Length < i)
                {
                    if (rand < probab[i]) return options[i];
                    rand -= probab[i];
                }
                else
                {
                    rand--;
                }
            }
            return options.Last();
        }
    }
}
