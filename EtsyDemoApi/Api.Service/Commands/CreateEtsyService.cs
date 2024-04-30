
//using Api.Data.Repository.Commands;
//using Api.Data.Repository.Queries;
//using Api.Domain.Enum;
//using Api.Domain.Request;
//using Api.Domain.Response;
//using Api.Infraestructura.Context;
//using Microsoft.Extensions.Configuration;

//namespace Api.Service.Commands
//{
//    public class CreateEtsyService : ICreateEtsyService
//    {
//        private readonly HttpClient? _httpClient;
//        private readonly string? _apiKey;
//        private readonly IEtsyRepository _etsyRepository;


//        public CreateEtsyService(HttpClient httpClient,
//                          IConfiguration configuration,
//                          ApiContext context,
//                          IEtsyRepository etsyRepository,
//                          IEtsyQuery etsyQyery)
//        {
//            _httpClient = httpClient;
//            _apiKey = configuration["EtsyApiKey"];
//            _etsyRepository = etsyRepository;
//        }

//        public async Task<Response> CreateProductAsync(CreateProductRequest createRequest)
//        {
//            Response response = new Response { Status = StatusType.SUCCESS };
//            if (response.Status == StatusType.ERROR)
//            {
//                return response;
//            }
//            response = await _etsyRepository.CreateProductAsync(createRequest);
//            return response;

//        }

//        public async Task<Response> CreateShopAsync(CreateRequest createRequest)
//        {
//            Response response = new Response { Status = StatusType.SUCCESS };
//            if (response.Status == StatusType.ERROR)
//            {
//                return response;
//            }
//            response = await _etsyRepository.CreateShopAsync(createRequest);
//            return response;
//        }
//    }
//}
