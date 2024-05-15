using Api.Domain.Response;

namespace Api.Data.Repository.Queries.Contracts
{
    public interface ICartQuery
    {
        Task<ResponseCart> GetCartAsync(int idUser);
    }
}
