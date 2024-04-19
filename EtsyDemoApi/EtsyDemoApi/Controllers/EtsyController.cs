using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Models;
using Api.Service.Etsy;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EtsyDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EtsyController : Controller
    {
        private readonly IEtsyService _etsyService;

        public EtsyController(IEtsyService etsyService)
        {
            _etsyService = etsyService;
        }

        //GET para obtener tiendas
        [HttpGet("shops")]
        [SwaggerOperation("Devolver listado de tiendas")]
        [ProducesResponseType(typeof(List<Shop>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetShops()
        {
            return await _etsyService.GetShopsAsync();
        }

        //GET productos por Tienda (id)
        [HttpGet("shops/{shopId}/products")]
        [SwaggerOperation("Devolver los productos de una tienda")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetProductsByShop(int shopId)
        {
            return await _etsyService.GetProductsByShopAsync(shopId);
        }


        //GET productos por nombre
        [HttpGet("products/search")]
        [SwaggerOperation("Buscar productos por nombre")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetProductsByName([FromQuery] string name)
        {
            return await _etsyService.GetProductsByNameAsync(name);
        }

        //GET tiendas por nombre 
        [HttpGet("shops/search")]
        [SwaggerOperation("Buscar tiendas por nombre")]
        [ProducesResponseType(typeof(List<Shop>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetShopsByName([FromQuery] string name)
        {
            return await _etsyService.GetShopsByNameAsync(name);
        }

        //POST crear Tienda para pruebas
        [HttpPost("shops/create")]
        [SwaggerOperation("Buscar tiendas por nombre")]
        [ProducesResponseType(typeof(List<Shop>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> Create(CreateRequest createRequest)
        {
            return await _etsyService.CreateShopAsync(createRequest);
        }


        //POST crear Producto para pruebas
        [HttpPost("product/create")]
        [SwaggerOperation("Buscar tiendas por nombre")]
        [ProducesResponseType(typeof(List<Shop>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response> CreateProduct(CreateProductRequest createRequest)
        {
            return await _etsyService.CreateProductAsync(createRequest);
        }
    }
}
