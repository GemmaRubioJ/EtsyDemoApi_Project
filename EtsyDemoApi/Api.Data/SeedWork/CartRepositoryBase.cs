using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Context;

namespace Api.Data.SeedWork
{
    public class CartRepositoryBase
    {
        protected readonly ResponseCart _responseCart = new ResponseCart() { Status = StatusType.SUCCESS };
        protected readonly ApiContext _context;
        protected readonly HttpClient? _httpClient;

        public CartRepositoryBase(ApiContext context, HttpClient? httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }
    }
}
