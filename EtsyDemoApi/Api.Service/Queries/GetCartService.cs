using Api.Data.Repository.Queries.Contracts;
using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Service.Queries.Contracts;

namespace Api.Service.Queries
{
    public  class GetCartService :IGetCartService
    {
        private readonly ICartQuery _cartQuery;

        public GetCartService(ICartQuery cartQuery)
        {
            _cartQuery = cartQuery;
        }

        public async Task<ResponseCart> GetCartAsync (int idUser)
        {
             return    await _cartQuery.GetCartAsync(idUser);

        }
    }
}
