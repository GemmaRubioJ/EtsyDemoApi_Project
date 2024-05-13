using Api.Domain.Request;
using Api.Domain.Response;

namespace Api.Data.Repository.Commands.Contracts
{
    public interface ICartRepository
    {
        Task<ResponseCart> CreateCartAsync(CartRequest request);
    }
}
