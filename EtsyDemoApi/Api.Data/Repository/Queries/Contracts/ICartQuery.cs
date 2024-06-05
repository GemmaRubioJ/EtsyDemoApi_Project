using Api.Domain.Response;

namespace Api.Data.Repository.Queries.Contracts
{
    public interface ICartQuery
    {
        Task<ResponseCart> GetCartAsync(int idUser);
        Task<string> GetProductImageUrl(int productId);
        Task<string> GetProductName(int productId);
        Task<decimal> GetProductPrice(int productId);
    }
}
