using Api.Domain.Request;
using Api.Domain.Response;

namespace Api.Service.Commands
{
    public interface ICreateEtsyService
    {
        Task<ResponseUser> CreateUserAsync(RegisterUserRequest registerUserRequest);
        Task<ResponseUserToken> LogInUserAsync(LogInRequest logInRequest);
    }
}