using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Models;

namespace Api.Service.Commands.Contracts
{
    public interface IUserService
    {
        Task<ResponseUser> CreateUserAsync(RegisterUserRequest registerUserRequest);
        Task<ResponseUserToken> LogInUserAsync(LogInRequest logInRequest);

    }
}
