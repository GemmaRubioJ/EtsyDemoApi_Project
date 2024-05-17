using Api.Data.Repository.Commands.Contracts;
using Api.Data.Repository.Queries.Contracts;
using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.DTOs;
using Api.Infraestructura.Models;
using Api.Service.Commands.Contracts;


namespace Api.Service.Commands
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserQuery _userQuery;
        private readonly ICartQuery _cartQuery;

        public CartService(ICartRepository cartRepository,
                            IUserQuery userQuery,
                            ICartQuery cartQuery )
        {
            _cartRepository = cartRepository;
            _userQuery = userQuery;
            _cartQuery = cartQuery;
        }

        public async Task<ResponseCart> CreateCartAsync(CartRequest request)
        {

            return await _cartRepository.CreateCartAsync(request);

        }


        public async Task<ResponseCart> UpdateCartAsync(int idCart, UpdateCartRequest request)
        {

            return await _cartRepository.UpdateCartAsync(idCart, request);

        }


        public async Task<ResponseCart> DeleteCartAsync(int idCart)
        {

            return await _cartRepository.DeleteCartAsync(idCart);

        }

        public async Task<ResponseCheckout> ProccesCheckout(CartRequest request)
        {
            var response = new ResponseCheckout { Status = StatusType.SUCCESS };

            if (!ValidateRequest(request))
            {
                response.Status = StatusType.ERROR;
                response.Message = "Datos del carrito inválidos";
                return response;
            }

            try
            {
                response.Data = await PrepareEmailData(request);
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
                response.Status = StatusType.ERROR;
            }

            return response;
        }

        private bool ValidateRequest(CartRequest request)
        {
            return request != null && request.Products != null && request.Products.Any();
        }

        private async Task<EmailDataDto> PrepareEmailData(CartRequest request)
        {
            var user = await _userQuery.GetUserByIdAsync((int)request.UserId);
            var cartItems = new List<CartItemEmailDto>();
            foreach (var product in request.Products)
            {
                var productName = await _cartQuery.GetProductName(product.ProductId);
                var productPrice = await _cartQuery.GetProductPrice(product.ProductId);

                cartItems.Add(new CartItemEmailDto
                {
                    ProductId = product.ProductId,
                    Title = productName,
                    Quantity = product.Quantity,
                    Price = productPrice
                });
            }
            var emailData = new EmailDataDto
            {
                Email = user.Email,
                CartItems = cartItems
            };

            return emailData;
        }


    }
}
