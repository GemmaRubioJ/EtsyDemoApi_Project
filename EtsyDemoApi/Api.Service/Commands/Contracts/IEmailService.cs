using Api.Infraestructura.DTOs;

namespace Api.Service.Commands.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string userEmail, List<CartItemEmailDto> cartItems);
    }
}
