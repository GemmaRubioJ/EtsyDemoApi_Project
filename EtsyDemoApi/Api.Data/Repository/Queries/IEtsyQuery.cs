using Api.Domain.Response;

namespace Api.Data.Repository.Queries
{
    public interface IEtsyQuery
    {
        Task<ResponseShops> GetShopsAsync();
    }
}