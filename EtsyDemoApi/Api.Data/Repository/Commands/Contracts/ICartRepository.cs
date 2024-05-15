using Api.Domain.Request;
using Api.Domain.Response;

namespace Api.Data.Repository.Commands.Contracts
{
    public interface ICartRepository
    {
        Task<ResponseCart> CreateCartAsync(CartRequest request);

        Task<ResponseCart> UpdateCartAsync(int idCart, UpdateCartRequest request);
        Task<ResponseCart> DeleteCartAsync(int idCart);
    }
}
