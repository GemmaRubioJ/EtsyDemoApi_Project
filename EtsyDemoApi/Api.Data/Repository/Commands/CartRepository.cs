using Api.Data.Repository.Commands.Contracts;
using Api.Data.SeedWork;
using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;
using Microsoft.EntityFrameworkCore;

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
                    Date = DateTime.UtcNow, 
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
                _responseCart.Data = cart; 
            }
            catch (Exception ex)
            {
                _responseCart.Status = StatusType.ERROR;
                _responseCart.Message = "Error al crear el carrito: " + ex.Message;
            }

            return _responseCart;
        }


        public async Task<ResponseCart> UpdateCartAsync (int idCart, UpdateCartRequest request)
        {
            try
            {
                var cart = await _context.Carts.Include(c => c.Products)
                                               .FirstOrDefaultAsync(c => c.Id == idCart);
                if (cart == null)
                {
                    _responseCart.Status = StatusType.ERROR;
                    _responseCart.Message = "Carrito no encontrado";
                    return _responseCart;
                }
                // Actualizar productos en el carrito
                foreach (var item in request.Products)
                {
                    var cartItem = cart.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (cartItem != null)
                    {
                        if (item.Quantity == 0)
                        {
                            _context.CartItems.Remove(cartItem);
                        }
                        else
                        {
                            cartItem.Quantity = item.Quantity;
                        }
                    }
                    else if (item.Quantity > 0)
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
                            ProductId = item.ProductId, Quantity = item.Quantity, Cart = cart
                        });
                    }
                }
                await _context.SaveChangesAsync();
                _responseCart.Status = StatusType.SUCCESS;
                _responseCart.Message = "Carrito actualizado con éxito";
                _responseCart.Data = cart;
            }
            catch (Exception ex)
            {
                _responseCart.Status = StatusType.ERROR;
                _responseCart.Message = "Error al actualizar el carrito: " + ex.Message;
            }

            return _responseCart;
        }


        public async Task<ResponseCart> DeleteCartAsync(int idCart)
        {
            try
            {
                var cart = await _context.Carts
                                         .Include(c => c.Products) 
                                         .FirstOrDefaultAsync(c => c.Id == idCart);
                if (cart == null)
                {
                    _responseCart.Status = StatusType.ERROR;
                    _responseCart.Message = "Carrito no encontrado";
                    return _responseCart;
                }
                _context.CartItems.RemoveRange(cart.Products);
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();

                _responseCart.Status = StatusType.SUCCESS;
                _responseCart.Message = "Carrito eliminado con éxito";
            }
            catch (Exception ex)
            {
                // Manejo de errores
                _responseCart.Status = StatusType.ERROR;
                _responseCart.Message = $"Error al eliminar el carrito: {ex.Message}";
            }

            return _responseCart;
        }
    }
}
