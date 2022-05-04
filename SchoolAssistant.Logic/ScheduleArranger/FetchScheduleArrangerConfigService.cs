﻿using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger.PageModelToReact;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchScheduleArrangerConfigService
    {
        Task<ScheduleArrangerConfigJson> FetchAsync();
    }

    [Injectable]
    public class FetchScheduleArrangerConfigService : IFetchScheduleArrangerConfigService
    {
        private readonly IAppConfigRepository _configRepo;

        public FetchScheduleArrangerConfigService(
            IAppConfigRepository configRepo)
        {
            _configRepo = configRepo;
        }

        public async Task<ScheduleArrangerConfigJson> FetchAsync()
        {
            return new ScheduleArrangerConfigJson
            {
                defaultLessonDuration = await _configRepo.DefaultLessonDuration.GetAsync() ?? 45,
                cellDuration = await _configRepo.ScheduleArrangerCellDuration.GetAsync() ?? 5,
                cellHeight = await _configRepo.ScheduleArrangerCellHeight.GetAsync() ?? 5,
                startHour = await _configRepo.ScheduleStartHour.GetAsync() ?? 7,
                endHour = await _configRepo.ScheduleEndhour.GetAsync() ?? 18
            };
        }
    }
}
