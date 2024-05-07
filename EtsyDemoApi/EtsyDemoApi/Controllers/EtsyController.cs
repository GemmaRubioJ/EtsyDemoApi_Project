using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Models;

using Api.Service.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EtsyDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EtsyController : Controller
    {

        private readonly IGetEtsyService _getEtsyService;

        public EtsyController(IGetEtsyService getEtsyService) {
        
            _getEtsyService = getEtsyService;
        }

            ////GET para obtener tiendas
            //[HttpGet("shops")]
            //[SwaggerOperation("Devolver listado de tiendas")]
            //[ProducesResponseType(typeof(List<Shop>), 200)]
            //[ProducesResponseType(StatusCodes.Status400BadRequest)]
            //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
            //public async Task<Response> GetShops()
            //{
            //    return await _getEtsyService.GetShopsAsync();
            //}

            ////GET productos por Tienda (id)
            //[HttpGet("shops/{shopId}/products")]
            //[SwaggerOperation("Devolver los productos de una tienda")]
            //[ProducesResponseType(typeof(List<Product>), 200)]
            //[ProducesResponseType(StatusCodes.Status400BadRequest)]
            //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
            //public async Task<Response> GetProductsByShop(int shopId)
            //{
            //    return await _getEtsyService.GetProductsByShopAsync(shopId);
            //}


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

        //GET tiendas por nombre 
        //[HttpGet("shops/search/{name}")]
        //[SwaggerOperation("Buscar tiendas por nombre")]
        //[ProducesResponseType(typeof(List<Shop>), 200)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<Response> GetShopsByName(string name)
        //{
        //    return await _getEtsyService.GetShopsByNameAsync(name);
        //}

        ////POST crear Tienda para pruebas
        //[HttpPost("shops/create")]
        //[SwaggerOperation("Crear Tienda para simulación de la Etsy Api")]
        //[ProducesResponseType(typeof(List<Shop>), 200)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<Response> CreateShop(CreateRequest createRequest)
        //{
        //    return await _createEtsyService.CreateShopAsync(createRequest);
        //}


        ////POST crear Producto para pruebas
        //[HttpPost("product/create")]
        //[SwaggerOperation("Crear Producto para simulación de la Etsy Api")]
        //[ProducesResponseType(typeof(List<Product>), 200)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<Response> CreateProduct(CreateProductRequest createRequest)
        //{
        //    return await _createEtsyService.CreateProductAsync(createRequest);
        //}

    }
}
