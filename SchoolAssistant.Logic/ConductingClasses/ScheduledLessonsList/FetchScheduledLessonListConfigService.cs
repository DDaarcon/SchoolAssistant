using AppConfigurationEFCore;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.ConductingClasses.ScheduledLessonsList
{
    public interface IFetchScheduledLessonListConfigService
    {
        Task<ScheduledLessonListConfigJson> GetDefaultConfigAsync();
    }

    [Injectable]
    public class FetchScheduledLessonListConfigService : IFetchScheduledLessonListConfigService
    {
        private readonly IAppConfiguration<AppConfigRecords> _appConfig;

        public FetchScheduledLessonListConfigService(
            IAppConfiguration<AppConfigRecords> appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<ScheduledLessonListConfigJson> GetDefaultConfigAsync()
        {
            return new ScheduledLessonListConfigJson
            {
                minutesBeforeLessonIsSoon = await _appConfig.Records.MinutesBeforeLessonConsideredClose.GetAsync().ConfigureAwait(false) ?? 5,
                topicLengthLimit = await _appConfig.Records.LessonsListTopicLengthLimit.GetAsync().ConfigureAwait(false) ?? 70
            };
        }
    }
}
