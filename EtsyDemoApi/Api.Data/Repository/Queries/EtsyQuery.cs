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


        /// <summary>
        /// Realiza consulta HTTP para recuperar productos de Api Externa que coincidan con una cadena
        /// de texto
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// Obtiene todos los productos de la Api externa 
        /// </summary>
        /// <returns></returns>
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
                        // Guardar o actualizar los productos en la base de datos
                        foreach (var product in products)
                        {
                            var existingProduct = await _context.Products
                                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
                            if (existingProduct == null)
                            {
                                _context.Products.Add(product); // Agregar si no existe
                            }
                        }
                        await _context.SaveChangesAsync();
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
        /// Recupera producto de la Api externa que coincida con un Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseProduct> GetProductByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync($"https://fakestoreapi.com/products/{id}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<Product>(json);
                    if (product == null)
                    {
                        _responseProduct.Status = StatusType.ERROR;
                        _responseProduct.Message = "No se encontró el producto";
                        return _responseProduct;
                    }
                    else
                    {
                        _responseProduct.Status = StatusType.SUCCESS;
                        _responseProduct.Message = "Producto recolectado exitosamente";
                        _responseProduct.Data = product;
                    }
                }
                else
                {
                    _responseProduct.Status = StatusType.ERROR;
                    _responseProduct.Message = "Error en la respuesta de la API";
                }
            }
            catch (Exception ex)
            {
                _responseProduct.Message += ex.Message;
                _responseProduct.Status = StatusType.ERROR;
            }
            return _responseProduct;
        }
    }
    
}
