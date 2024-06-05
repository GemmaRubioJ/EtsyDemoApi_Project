using SendGrid;
using SendGrid.Helpers.Mail;
using Api.Service.Commands.Contracts;
using Microsoft.Extensions.Logging;
using Api.Infraestructura.DTOs;
using Api.Domain.Response;
using System.Text;


namespace Api.Service.Commands
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SendGridClient _client;

        public EmailService (string apiKey , ILogger<EmailService> logger )
        {
            _logger = logger;
            _client = new SendGridClient(apiKey);
        }


        /// <summary>
        /// Envía correo electrónico de confirmación al usuario
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string userEmail, List<CartItemEmailDto> cartItems)
        {
            var subject = "Confirmación de tu compra";
            var htmlContent = BuildHtmlEmailContent(cartItems);
            var msg = MailHelper.CreateSingleEmail(new EmailAddress("gemi.816@gmail.com"), new EmailAddress(userEmail), subject, "", htmlContent);
            var response = await _client.SendEmailAsync(msg);
            // Puedes manejar la respuesta de SendGrid según necesites
        }


        /// <summary>
        /// Construye el contenido HTML para el correo de confirmación
        /// </summary>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        private string BuildHtmlEmailContent(List<CartItemEmailDto> cartItems)
        {
            var totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<html>");
            stringBuilder.Append("<head>");
            stringBuilder.Append("<style>");
            stringBuilder.Append("body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }");
            stringBuilder.Append(".email-container { max-width: 600px; margin: auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); }");
            stringBuilder.Append("h1 { color: #333333; }");
            stringBuilder.Append("p { color: #666666; }");
            stringBuilder.Append(".product { border-bottom: 1px solid #dddddd; padding: 10px 0; }");
            stringBuilder.Append(".product:last-child { border-bottom: none; }");
            stringBuilder.Append(".product-name { font-weight: bold; color: #333333; text-align: center; }");
            stringBuilder.Append(".product-quantity, .product-price { color: #666666; text-align: center; }");
            stringBuilder.Append(".total { font-weight: bold; margin-top: 20px; text-align: center; }");
            stringBuilder.Append("table { width: 100%; max-width: 100%; table-layout: fixed; margin-top: 20px; }");
            stringBuilder.Append("th, td { text-align: center; padding: 8px; border-bottom: 1px solid #ccc; font-size: 19px; }");
            stringBuilder.Append("button { background-color: #E79644; color: white; border: none; padding: 10px 20px; text-align: center; display: inline-block; font-size: 16px; margin: 4px 2px; transition: background 0.3s ease; cursor: pointer; border-radius: 20px; }");
            stringBuilder.Append("button:hover { background-color: #E9B270; }");
            stringBuilder.Append("</style>");
            stringBuilder.Append("</head>");
            stringBuilder.Append("<body>");
            stringBuilder.Append("<div class='email-container'>");
            stringBuilder.Append("<h1>!Gracias por tu compra!</h1>");
            stringBuilder.Append("<h3>Detalles del pedido:</h3>");
            stringBuilder.Append("<table>");
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th>Producto</th>");
            stringBuilder.Append("<th>Cantidad</th>");
            stringBuilder.Append("<th>Precio</th>");
            stringBuilder.Append("</tr>");

            foreach (var item in cartItems)
            {
                stringBuilder.Append("<tr class='product'>");
                stringBuilder.AppendFormat("<td class='product-name'>{0}</td>", item.Title);
                stringBuilder.AppendFormat("<td class='product-quantity'>{0}</td>", item.Quantity);
                stringBuilder.AppendFormat("<td class='product-price'>${0}</td>", item.Price);
                stringBuilder.Append("</tr>");
            }

            stringBuilder.Append("</table>");
            stringBuilder.AppendFormat("<div class='total'>Total: ${0}</div>", totalPrice);
            stringBuilder.Append("</div>"); 
            stringBuilder.Append("</body>");
            stringBuilder.Append("</html>");

            return stringBuilder.ToString();
        }
    }
}
