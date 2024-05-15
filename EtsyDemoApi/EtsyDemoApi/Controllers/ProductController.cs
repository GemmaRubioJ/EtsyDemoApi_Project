using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Models;
using Api.Service.Commands;
using Api.Service.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EtsyDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        private readonly IGetEtsyService _getEtsyService;

        public ProductController(IGetEtsyService getEtsyService) {
        
            _getEtsyService = getEtsyService;
        }



        //GET productos por nombre
        [HttpGet("products/search/{name}")]
        [SwaggerOperation("Buscar productos por nombre")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetProductsByName(string name)
        {
            return await _getEtsyService.GetProductsByNameAsync(name);
        }

        //GET TODOS PRODUCTOS FAKE STORE API
        [HttpGet("products/all")]
        [SwaggerOperation("Buscar productos por nombre")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetAllProducts()
        {
        return await _getEtsyService.GetProductsAsync();
        }

        //GET PRODUCTO POR ID FAKE STORE API
        [HttpGet("products/{id}")]
        [SwaggerOperation("Buscar producto por id")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetAllProducts(int id)
        {
            return await _getEtsyService.GetProductByIdAsync(id);
        }

        //GET TODOS USUARIOS FAKE STORE API
        [HttpGet("users/all")]
        [SwaggerOperation("Buscar todos los usuarios")]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetAllUsers()
        {
        return await _getEtsyService.GetUsersAsync();
        }

    }
}
