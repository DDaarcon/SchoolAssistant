using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.UsersManagement.FetchUserRelatedObjectsHelp;

namespace SchoolAssistant.Logic.UsersManagement
{
    public interface IFetchUserRelatedObjectsService
    {
        Task<SimpleRelatedObjectJson[]> GetObjectsAsync(FetchRelatedObjectsRequestJson model);
    }

    [Injectable]
    public class FetchUserRelatedObjectsService : IFetchUserRelatedObjectsService
    {
        private IFetchStudentUserRelatedObjectsService _fetchStudentsService;

        public FetchUserRelatedObjectsService(
            IFetchStudentUserRelatedObjectsService fetchStudentsService)
        {
            _fetchStudentsService = fetchStudentsService;
        }

        public async Task<SimpleRelatedObjectJson[]> GetObjectsAsync(FetchRelatedObjectsRequestJson model)
        {
            return model.ofType switch
            {
                UserTypeForManagement.Student => await _fetchStudentsService.GetAsync(model),
                UserTypeForManagement.Teacher => throw new NotImplementedException(),
                UserTypeForManagement.Administration => throw new NotImplementedException(),
                UserTypeForManagement.Headmaster => throw new NotImplementedException(),
                UserTypeForManagement.SystemAdmin => throw new NotImplementedException(),
                UserTypeForManagement.Parent => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
