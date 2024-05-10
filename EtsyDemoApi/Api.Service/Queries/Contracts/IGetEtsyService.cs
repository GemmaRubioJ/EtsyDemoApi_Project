using Api.Domain.Request;
using Api.Domain.Response;

namespace Api.Service.Queries
{
    public interface IGetEtsyService
    {
        Task<ResponseProducts> GetProductsByNameAsync(string name);
        Task<ResponseProducts> GetProductsAsync();
        Task<ResponseUsers> GetUsersAsync();
        Task<ResponseProduct> GetProductByIdAsync(int id);

    }
}