
using Api.Infraestructura.Models;
using Api.Domain;
using Api.Domain.Response;
using Microsoft.Extensions.Configuration;
using Api.Infraestructura.Context;
using Api.Domain.Request;
using Api.Domain.Enum;
using System.ComponentModel;
using Api.Data.Repository.Commands;
using Api.Data.Repository.Queries;

namespace Api.Service.Etsy
{
    public class EtsyService : IEtsyService
    {
        private readonly HttpClient? _httpClient;
        private readonly string? _apiKey;
        private readonly ApiContext _context;
        private readonly IEtsyRepository _etsyRepository;
        private readonly IEtsyQuery _etsyQuery;

        public EtsyService(HttpClient _httpClient,
                          IConfiguration configuration, 
                          ApiContext context,
                          IEtsyRepository etsyRepository,
                          IEtsyQuery etsyQyery)
        {
            _httpClient = _httpClient ?? throw new ArgumentNullException();
            _apiKey = configuration["EtsyApiKey"];
            _context = context;
            _etsyRepository = etsyRepository;
            _etsyQuery = etsyQyery;
        }


        public async Task<Response> GetShopsAsync()
        {
            Response response = new Response { Status = StatusType.SUCCESS };
            if (response.Status == StatusType.ERROR)
            {
                return response;
            }
            response = await _etsyQuery.GetShopsAsync();
            return response;
        }

        public Task<Response> GetProductsByShopAsync(int shopId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetProductsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetShopsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> CreateShopAsync(CreateRequest createRequest)
        {
            Response response = new Response { Status = StatusType.SUCCESS };
            if (response.Status == StatusType.ERROR)
            {
                return response;
            }
            response = await _etsyRepository.CreateShopAsync(createRequest);
            return response;
        }

        public async Task<Response> CreateProductAsync(CreateProductRequest createRequest)
        {
            Response response = new Response { Status = StatusType.SUCCESS };
            if (response.Status == StatusType.ERROR)
            {
                return response;
            }
            response = await _etsyRepository.CreateProductAsync(createRequest);
            return response;

        }
    }
}
