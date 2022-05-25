using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement
{
    public interface IAddUserService
    {
        Task<AddUserResponseJson> AddAsync(AddUserRequestJson model);
    }

    [Injectable]
    public class AddUserService : IAddUserService
    {


        private AddUserRequestJson _model = null!;


        public async Task<AddUserResponseJson> AddAsync(AddUserRequestJson model)
        {
            return null;
        }
    }
}
