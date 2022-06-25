using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.MarksOverview;
using SchoolAssistant.Infrastructure.Models.ScheduleDisplay;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic.ScheduleDisplay;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    [Authorize(Roles = "Student")]
    public class StudentModel : MyPageModel
    {
        private readonly IFetchSchedDisplayConfigService _fetchScheduleConfigSvc;
        private readonly IStudentScheduleService _scheduleSvc;


        private Student? _student;

        public ScheduleConfigJson ScheduleConfig { get; set; } = null!;
        public ScheduleDayLessonsJson[] ScheduleLessons { get; set; } = null!;

        public MarksOverviewModel MarksOverview { get; set; } = new MarksOverviewModel();

        public StudentModel(
            IUserRepository userRepo,
            IFetchSchedDisplayConfigService fetchScheduleConfigSvc,
            IStudentScheduleService scheduleSvc) : base(userRepo)
        {
            _fetchScheduleConfigSvc = fetchScheduleConfigSvc;
            _scheduleSvc = scheduleSvc;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            if (!await FetchAndValidateIfUserOfTypeAsync(DAL.Enums.UserType.Student).ConfigureAwait(false))
                return RedirectToStart;

            if (!FetchAndValidateStudentForCurrentYear())
                throw new Exception("Not found student for a current SchoolYear in the database");

            ScheduleConfig = await _fetchScheduleConfigSvc.FetchForAsync(_User!).ConfigureAwait(false);

            ScheduleLessons = _scheduleSvc.GetModel(_student!)!;


            MarksOverview.Marks = new List<MarkForOverviewModel>()
            {
                new()
                {
                    Mark = "-5", Subject = "English", Issuer = "Tomasz Kowalczyk"
                },
                new()
                {
                    Mark = "1", Subject = "Polish", Issuer = "Tomasz Kowalczyk"
                },
                new()
                {
                    Mark = "+3", Subject = "Math", Issuer = "Tomasz Kowalczyk"
                },
                new()
                {
                    Mark = "-5", Subject = "Some long subject name to check long names", Issuer = "Tomasz Kowalczykowiañskowski"
                }
            };

            return Page();
        }

        private bool FetchAndValidateStudentForCurrentYear()
        {
            _student = _User!.Student?.StudentInstances.FirstOrDefault(x => x.SchoolYear.Current);

            return _student is not null;
        }
    }
}
