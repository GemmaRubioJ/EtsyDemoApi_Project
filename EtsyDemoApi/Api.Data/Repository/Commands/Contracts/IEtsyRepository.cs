using Api.Domain.Request;
using Api.Domain.Response;

namespace Api.Data.Repository.Commands
{
    public interface IEtsyRepository
    {
        Task<Response> CreateProductAsync(CreateProductRequest createRequest);
        Task<ResponseUser> RegisterUserAsync(RegisterUserRequest registerUserRequest);
    }
}