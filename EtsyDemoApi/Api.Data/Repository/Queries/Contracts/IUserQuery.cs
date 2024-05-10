using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Models;

namespace Api.Data.Repository.Queries.Contracts
{
    public interface IUserQuery
    {
        Task<ResponseUsers> GetUsersAsync();
        Task SaveUsersAsync(IEnumerable<User> users);
        Task<IEnumerable<string>> GetExistingUserEmailsAsync();
        Task<User> GetUserByEmailOrUsernameAsync(string email, string username);
        Task<User> GetUserByEmail(string email);
        Task<ResponseUserToken> LogInUserAsync(LogInRequest logInRequest);

    }
}
