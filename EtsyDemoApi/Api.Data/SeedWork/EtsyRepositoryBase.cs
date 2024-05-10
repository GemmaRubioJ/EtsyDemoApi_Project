using Api.Data.Repository.Queries;
using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Context;

namespace Api.Data.SeedWork
{
    public class EtsyRepositoryBase
    {
        protected readonly Response _response = new Response() { Status = StatusType.SUCCESS };
        protected readonly ResponseUser _responseUser = new ResponseUser() { Status = StatusType.SUCCESS };
        protected readonly ResponseUsers _responseUsers = new ResponseUsers() { Status = StatusType.SUCCESS };
        protected readonly ResponseProduct _responseProduct = new ResponseProduct() {  Status = StatusType.SUCCESS };
        protected readonly ResponseProducts _responseProducts = new  ResponseProducts() { Status = StatusType.SUCCESS };
        protected readonly ResponseUserToken _responseUserToken = new ResponseUserToken() { Status = StatusType.SUCCESS };


        protected readonly ApiContext _context;

        protected readonly HttpClient? _httpClient;

        public EtsyRepositoryBase( HttpClient? httpClient, ApiContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }
    }
}
