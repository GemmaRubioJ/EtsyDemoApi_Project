using Api.Data.SeedWork;
using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;
using Azure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;

namespace Api.Data.Repository.Queries
{
    public class EtsyQuery : EtsyRepositoryBase, IEtsyQuery
    {
        public EtsyQuery( HttpClient httpclient, ApiContext context) : base( httpclient, context) { }


        //public async Task<ResponseShops> GetShopsAsync()
        //{
        //    try
        //    {
        //        List<Shop> shops = await _context.Shops.Include(shop => shop.Products).ToListAsync();
        //        if (!shops.Any())
        //        {
        //            _responseShops.Status = StatusType.ERROR;
        //            _responseShops.Message = "Error al recolectar las tiendas";
        //            return _responseShops;
        //        }
        //        else
        //        {
        //            _responseShops.Status = StatusType.SUCCESS;
        //            _responseShops.Message = "Tiendas recolectadas exitosamente";
        //            _responseShops.Data = shops;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _responseShops.Message += ex.Message;
        //        _responseShops.Status = StatusType.ERROR;
        //    }
        //    return _responseShops;
        //}


        //public async Task<ResponseShops> GetShopsByNameAsync(string name)
        //{
        //    try
        //    {
        //        IEnumerable<Shop> shops =  await _context.Shops.Where(shop => shop.Name.Contains(name)).ToListAsync();
        //        if (!shops.Any())
        //        {
        //            _responseShops.Status = StatusType.ERROR;
        //            _responseShops.Message = "Error al recolectar las tiendas";
        //            return _responseShops;
        //        }
        //        else
        //        {
        //            _responseShops.Status = StatusType.SUCCESS;
        //            _responseShops.Message = "Tiendas recolectadas exitosamente";
        //            _responseShops.Data = shops;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _responseShops.Message += ex.Message;
        //        _responseShops.Status = StatusType.ERROR;
        //    }
        //    return _responseShops;
        //}




        /// <summary>
        /// Recupera todos los productos disponibles de la API externa.
        /// Este método realiza una solicitud HTTP GET a la API y deserializa la respuesta en una lista de productos.
        /// Si la respuesta es exitosa y contiene productos, retorna un estado de éxito con los productos;
        /// de lo contrario, retorna un estado de error con un mensaje apropiado.
        /// </summary>
        /// <returns>Una tarea que resulta en un objeto ResponseProducts que contiene el estado de la operación,
        /// los productos recuperados y un mensaje descriptivo.</returns>
        public async Task<ResponseProducts> GetProductsByNameAsync(string name)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync("https://fakestoreapi.com/products");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                    products = products.Where(p => p.Title.Contains(name, StringComparison.OrdinalIgnoreCase));
                    if (!products.Any())
                    {
                        _responseProducts.Status = StatusType.ERROR;
                        _responseProducts.Message = "Error al recolectar los productos";
                        return _responseProducts;
                    }
                    else
                    {
                        _responseProducts.Status = StatusType.SUCCESS;
                        _responseProducts.Message = "Productos recolectados exitosamente";
                        _responseProducts.Data = products;
                    }
                }
                else
                {
                    _responseProducts.Status = StatusType.ERROR;
                    _responseProducts.Message = "Error en la respuesta de la API";
                }
            }
            catch (Exception ex)
            {
                _responseProducts.Message += ex.Message;
                _responseProducts.Status = StatusType.ERROR;
            }
            return _responseProducts;
        }

        /// <summary>
        /// Busca productos por nombre en la API externa.
        /// Realiza una solicitud HTTP GET, filtra los productos por el nombre proporcionado y maneja la respuesta.
        /// Si no encuentra productos con el nombre especificado, retorna un error; si encuentra, retorna éxito.
        /// </summary>
        /// <param name="name">El nombre del producto a buscar.</param>
        /// <returns>Una tarea que resulta en un objeto ResponseProducts que contiene el estado de la operación,
        /// los productos filtrados y un mensaje descriptivo.</returns>
        public async Task<ResponseProducts> GetAllProductsAsync()
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync("https://fakestoreapi.com/products");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                    if (!products.Any())
                    {
                        _responseProducts.Status = StatusType.ERROR;
                        _responseProducts.Message = "Error al recolectar los productos";
                        return _responseProducts;
                    }
                    else
                    {
                        _responseProducts.Status = StatusType.SUCCESS;
                        _responseProducts.Message = "Productos recolectados exitosamente";
                        _responseProducts.Data = products;
                    }
                }
                else
                {
                    _responseProducts.Status = StatusType.ERROR;
                    _responseProducts.Message = "Error en la respuesta de la API";
                }
            }
            catch (Exception ex)
            {
                _responseProducts.Message += ex.Message;
                _responseProducts.Status = StatusType.ERROR;
            }
            return _responseProducts;
        }
    }
    
}
