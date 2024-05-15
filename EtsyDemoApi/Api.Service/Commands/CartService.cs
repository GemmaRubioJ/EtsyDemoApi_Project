using Api.Data.Repository.Commands.Contracts;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Service.Commands.Contracts;


namespace Api.Service.Commands
{
    public class CartService :ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ResponseCart> CreateCartAsync ( CartRequest request)
        {

            return await _cartRepository.CreateCartAsync(request);

        }


        public async Task<ResponseCart> UpdateCartAsync (int idCart, UpdateCartRequest request)
        {

            return await _cartRepository.UpdateCartAsync(idCart, request);

        }


        public async Task<ResponseCart> DeleteCartAsync(int idCart)
        {

            return await _cartRepository.DeleteCartAsync(idCart);

        }

    }
}
