using Api.Domain.Enum;
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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userSerive)
        {
            _userService = userSerive;
        }

        //GET Login
        [HttpPost("login")]
        [SwaggerOperation("Buscar todos los usuarios")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LogIn([FromBody] LogInRequest logInRequest)
        {
            var response = await _userService.LogInUserAsync(logInRequest);
            if (response.Status == StatusType.ERROR)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        //POST Registrar Usuario
        [HttpPost("register")]
        [SwaggerOperation("Registro de Usuario")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            return await _userService.CreateUserAsync(registerUserRequest);
        }
    }
}
