using Api.Data.Repository.Queries.Contracts;
using Api.Data.SeedWork;
using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository.Queries
{
    public class CartQuery : CartRepositoryBase, ICartQuery
    {
        public CartQuery(ApiContext context, HttpClient httpClient) : base(context, httpClient) { }

        public async Task<ResponseCart> GetCartAsync(int idUser)
        {
            try
            {
                var cart = await _context.Carts
                                .Where(c => c.UserId == idUser)
                                .Include(c => c.Products) 
                                .OrderByDescending(c => c.Date)  
                                .FirstOrDefaultAsync();
                if (cart == null)
                {
                    _responseCart.Status = StatusType.ERROR;
                    _responseCart.Message = "Carrito no  encontrado";
                    return _responseCart;
                }
                _responseCart.Status = StatusType.SUCCESS;
                _responseCart.Message = "Carrito encontrado";
                _responseCart.Data = cart;

            }catch (Exception ex)
            {
                _responseCart.Message += ex.Message;
                _responseCart.Status = StatusType.ERROR;
            }
            return _responseCart;
        }
    }
}
