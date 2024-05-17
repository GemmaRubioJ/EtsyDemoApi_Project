using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.DTOs;
using Api.Infraestructura.Models;
using Api.Service.Commands.Contracts;
using Api.Service.Queries.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EtsyDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IGetCartService _getCartService;
        private readonly IEmailService _emailService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, 
                                IGetCartService getCartService,
                                ILogger<CartController> logger,
                                IEmailService emailService)
        {
            _cartService = cartService;
            _getCartService = getCartService;
            _logger = logger;
            _emailService = emailService;
        }

        //POST Registrar un carrito 
        [HttpPost("register")]
        [SwaggerOperation("Registro de Carrito")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCart([FromBody] CartRequest cartRequest)
        {
            _logger.LogInformation("Recibiendo solicitud para crear carrito con datos: {@CartRequest}", cartRequest);
            try
            {
                var response = await _cartService.CreateCartAsync(cartRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el carrito");
                return StatusCode(500, "Internal Server Error");
            }
        }

        //POST modificar un carrito 
        [HttpPut("update/{id}")]
        [SwaggerOperation("Update de Carrito")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseCart> UpdateCart(int id, [FromBody] UpdateCartRequest cartRequest)
        {
            return await _cartService.UpdateCartAsync(id, cartRequest);
        }

        //POST Registrar un carrito 
        [HttpDelete("delete/{idCart}")]
        [SwaggerOperation("Borrado de Carrito")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseCart> DeleteCart(int idCart)
        {
            return await _cartService.DeleteCartAsync(idCart);
        }

        //GET get carrito por Id de usuario
        [HttpGet("{idUser}")]
        [SwaggerOperation("Borrado de Carrito")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseCart> GetCart(int idUser)
        {
            return await _getCartService.GetCartAsync(idUser);
        }


        //POST info del carrito
        [HttpPost("checkout")]
        [SwaggerOperation("Envío info del carrito")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Checkout(CartRequest request)
        {
            var response = await _cartService.ProccesCheckout(request);

            if (response.Status == StatusType.SUCCESS)
            {
                await _emailService.SendEmailAsync(response.Data.Email, response.Data.CartItems);
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
