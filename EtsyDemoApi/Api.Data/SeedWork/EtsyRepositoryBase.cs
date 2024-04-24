using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Context;

namespace Api.Data.SeedWork
{
    public class EtsyRepositoryBase
    {
        protected readonly Response _response = new Response() { Status = StatusType.SUCCESS };
        protected readonly ResponseShop _responseShop = new ResponseShop() { Status = StatusType.SUCCESS };
        protected readonly ResponseShops _responseShops = new ResponseShops() { Status = StatusType.SUCCESS };
        protected readonly ResponseProduct _responseProduct = new ResponseProduct() {  Status = StatusType.SUCCESS };
        protected readonly ResponseProducts _responseProducts = new  ResponseProducts() { Status = StatusType.SUCCESS };

        protected readonly ApiContext _context;
        protected readonly HttpClient? _httpClient;

        public EtsyRepositoryBase(ApiContext context, HttpClient? httpClient)
        {
            _context = context;
            _httpClient = httpClient;

        }
    }
}
