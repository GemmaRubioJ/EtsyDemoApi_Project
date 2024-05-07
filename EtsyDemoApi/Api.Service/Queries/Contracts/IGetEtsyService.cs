using Api.Domain.Response;

namespace Api.Service.Queries
{
    public interface IGetEtsyService
    {
        Task<ResponseProducts> GetProductsByNameAsync(string name);
        //Task<Response> GetProductsByShopAsync(int shopId);
        //Task<Response> GetShopsAsync();
        //Task<Response> GetShopsByNameAsync(string name);
        Task<ResponseProducts> GetProductsAsync();
        Task<ResponseUsers> GetUsersAsync();
        Task<ResponseProduct> GetProductByIdAsync(int id);
    }
}