using Api.Infraestructura.DTOs;
using Api.Infraestructura.Models;

namespace Api.Domain.Request
{
    public class CartRequest
    {
        public int? UserId { get; set; } 
        public List<CartItemDto> Products { get; set; }
    }
}
