using Api.Domain.Request;
using Api.Domain.Response;

namespace Api.Data.Repository.Commands
{
    public interface IEtsyRepository
    {
        Task<ResponseUser> RegisterUserAsync(RegisterUserRequest registerUserRequest);
    }
}