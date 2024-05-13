using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Models;
using Api.Service.Commands.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EtsyDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        //POST Registrar un carrito 
        [HttpPost("register")]
        [SwaggerOperation("Registro de Carrito")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseCart> RegisterUser([FromBody] CartRequest cartRequest)
        {
            return await _cartService.CreateCartAsync(cartRequest);
        }
    }
}
