using Api.Data.SeedWork;
using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository.Queries
{
    public class EtsyQuery : EtsyRepositoryBase, IEtsyQuery
    {
        public EtsyQuery(ApiContext context) : base(context) { }

        public async Task<ResponseShops> GetShopsAsync()
        {
            try
            {
                List<Shop> shops = await _context.Shops.Include(shop => shop.Products).ToListAsync();
                if (!shops.Any())
                {
                    _responseShops.Status = StatusType.ERROR;
                    _responseShops.Message = "Error al recolectar las tiendas";
                    return _responseShops;
                }
                else
                {
                    _responseShops.Status = StatusType.SUCCESS;
                    _responseShops.Message = "Tiendas recolectadas exitosamente";
                    _responseShops.Data = shops;
                }

            }
            catch (Exception ex)
            {
                _responseShops.Message += ex.Message;
                _responseShops.Status = StatusType.ERROR;
            }
            return _responseShops;
        }
    }
}
