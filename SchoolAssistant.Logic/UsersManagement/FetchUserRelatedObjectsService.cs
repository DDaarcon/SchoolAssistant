using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement
{
    public interface IFetchUserRelatedObjectsService
    {
        Task<SimpleRelatedObjectJson[]> GetObjectsAsync(FetchRelatedObjectsRequestJson model);
    }

    [Injectable]
    public class FetchUserRelatedObjectsService : IFetchUserRelatedObjectsService
    {

        private FetchRelatedObjectsRequestJson _model = null!;

        public async Task<SimpleRelatedObjectJson[]> GetObjectsAsync(FetchRelatedObjectsRequestJson model)
        {
            _model = model;

            return null;
        }
    }
}
