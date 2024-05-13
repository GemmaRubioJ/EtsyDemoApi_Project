using Api.Data.Repository.Commands.Contracts;
using Api.Data.SeedWork;
using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;

namespace Api.Data.Repository.Commands
{
    public class CartRepository : CartRepositoryBase, ICartRepository
    {
        public CartRepository(ApiContext context, HttpClient httpClient) : base(context, httpClient) { }

        public async Task<ResponseCart> CreateCartAsync(CartRequest request)
        {
            try
            {
                // Comprobar si el usuario existe
                var user = await _context.Users.FindAsync(request.UserId);
                if (user == null)
                {
                    _responseCart.Status = StatusType.ERROR;
                    _responseCart.Message = "Usuario no encontrado";
                    return _responseCart;
                }

                // Crear el carrito
                var cart = new Cart
                {
                    UserId = (int)request.UserId,
                    Date = DateTime.UtcNow, // Añade la fecha actual al crear el carrito
                    Products = new List<CartItem>()
                };

                // Añadir productos al carrito si existen en la request
                foreach (var item in request.Products)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                    {
                        _responseCart.Status = StatusType.ERROR;
                        _responseCart.Message = $"Producto con ID {item.ProductId} no encontrado.";
                        return _responseCart;
                    }
                    cart.Products.Add(new CartItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Cart = cart
                    });
                }

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();

                _responseCart.Status = StatusType.SUCCESS;
                _responseCart.Message = "Carrito creado con éxito";
                _responseCart.Data = cart; // Asume que Cart tiene una propiedad Id
            }
            catch (Exception ex)
            {
                _responseCart.Status = StatusType.ERROR;
                _responseCart.Message = "Error al crear el carrito: " + ex.Message;
            }

            return _responseCart;
        }
    }
}
