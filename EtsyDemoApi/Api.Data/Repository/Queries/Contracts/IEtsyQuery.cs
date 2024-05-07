using Api.Domain.Response;

namespace Api.Data.Repository.Queries
{
    public interface IEtsyQuery
    {
        //Task<ResponseShops> GetShopsAsync();
        //Task<ResponseShops> GetShopsByNameAsync(string name);
        Task<ResponseProducts> GetProductsByNameAsync(string name);
        Task<ResponseProducts> GetAllProductsAsync();
        Task<ResponseProduct> GetProductByIdAsync(int id);
    }
}