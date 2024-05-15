using Api.Domain.Request;
using Api.Domain.Response;

namespace Api.Service.Commands.Contracts
{
    public interface ICartService
    {
        Task<ResponseCart> CreateCartAsync(CartRequest request);
        Task<ResponseCart> DeleteCartAsync(int id);
        Task<ResponseCart> UpdateCartAsync(int idCart, UpdateCartRequest request);
    }
}
