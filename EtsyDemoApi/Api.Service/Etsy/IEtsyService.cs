using Api.Domain.Request;
using Api.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Etsy
{
    public interface IEtsyService
    {
        Task<Response> GetShopsAsync();
        Task<Response> GetProductsByShopAsync(int shopId);
        Task<Response> GetProductsByNameAsync(string name);
        Task<Response> GetShopsByNameAsync(string name);
        Task<Response> CreateShopAsync(CreateRequest createRequest);
        Task<Response> CreateProductAsync(CreateProductRequest createRequest);
    }
}
