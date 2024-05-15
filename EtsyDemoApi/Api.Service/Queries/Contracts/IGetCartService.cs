using Api.Domain.Response;

namespace Api.Service.Queries.Contracts
{
    public interface IGetCartService
    {
        Task<ResponseCart> GetCartAsync(int idUser);
    }
}
