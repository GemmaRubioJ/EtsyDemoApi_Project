using Api.Data.SeedWork;
using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;
using System;

namespace Api.Data.Repository.Commands
{
    public class EtsyRepository : EtsyRepositoryBase, IEtsyRepository
    {
        public EtsyRepository(ApiContext context, HttpClient httpClient) : base(context, httpClient) { }


        public async Task<Response> CreateShopAsync(CreateRequest createRequest)
        {

            try
            {
                if (createRequest is null)
                {
                    _response.Status = StatusType.ERROR;
                    _response.Message = "La solicitud no es correcta";
                    return _response;
                }
                else
                {
                    Shop shop = new Shop
                    {
                        Name = createRequest.Name,
                        Description = createRequest.Description
                    };
                    _context.Shops.Add(shop);
                    _response.Message = "Se añadió la tienda correctamente";
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.Status = StatusType.ERROR;
            }

            await _context.SaveChangesAsync();
            return _response;
        }

        public async Task<Response> CreateProductAsync(CreateProductRequest createRequest)
        {
            try
            {
                if (createRequest is null)
                {
                    _response.Status = StatusType.ERROR;
                    _response.Message = "La solicitud no es correcta";
                    return _response;
                }
                else
                {
                    Product product = new Product
                    {
                        Title = createRequest.Title,
                        Description = createRequest.Description,
                        Price = createRequest.Price,
                        //ShopId = createRequest.ShopId,
                    };
                    _context.Products.Add(product);
                    _response.Message = "Se añadió el Producto correctamente";

                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.Status = StatusType.ERROR;
            }
            await _context.SaveChangesAsync();
            return _response;
        }
    }

}
