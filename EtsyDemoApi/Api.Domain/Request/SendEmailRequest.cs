using Api.Infraestructura.DTOs;

namespace Api.Domain.Request
{
    public class SendEmailRequest
    {
        public string ToEmail { get; set; }
        public List<CartItemEmailDto> CartItems { get; set; }
    }
}
