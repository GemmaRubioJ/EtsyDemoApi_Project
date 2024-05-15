using Api.Infraestructura.DTOs;

namespace Api.Domain.Request
{
    public class UpdateCartRequest
    {
        public List<CartItemDto> Products { get; set; }
    }
}
