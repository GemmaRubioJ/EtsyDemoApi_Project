using Api.Data.Repository.Queries;
using Api.Data.Repository.Queries.Contracts;
using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Service.Queries
{
    public class GetEtsyService : IGetEtsyService
    {

        private readonly IEtsyQuery _etsyQuery;
        private readonly IUserQuery _userQuery;

        public GetEtsyService(IEtsyQuery etsyQyery, IUserQuery userQuery)
        {

            _etsyQuery = etsyQyery;
            _userQuery = userQuery;
        }



        /// <summary>
        /// Obtiene todos los productos disponibles a través de la interfaz IEtsyQuery.
        /// Este método inicialmente configura una respuesta con estado SUCCESS.
        /// Si la respuesta cambia a ERROR durante la obtención de datos, se retorna inmediatamente la respuesta.
        /// De lo contrario, procede a obtener los productos a través del método GetAllProductsAsync de IEtsyQuery y retorna la respuesta obtenida.
        /// </summary>
        /// <returns> ResponseProducts, que contiene el estado de la operación y, potencialmente, los productos recuperados.</returns>
        public async Task<ResponseProducts> GetProductsAsync()
        {
            ResponseProducts response = new ResponseProducts { Status = StatusType.SUCCESS };
            if (response.Status == StatusType.ERROR)
            {
                return response;
            }
            response = await _etsyQuery.GetAllProductsAsync();
            return response;
        }



        /// <summary>
        /// Busca productos por nombre utilizando la interfaz IEtsyQuery.
        /// Este método configura una respuesta con estado SUCCESS.
        /// Si la respuesta cambia a ERROR en algún punto durante la búsqueda de datos, la respuesta se retorna inmediatamente.
        /// Si no, continúa y realiza la búsqueda por nombre a través del método GetProductsByNameAsync de IEtsyQuery y retorna la respuesta obtenida.
        /// </summary>
        /// <param name="name">El nombre del producto a buscar.</param>
        /// <returns>ResponseProducts, que contiene el estado de la operación y los productos que coinciden con el criterio de búsqueda.</returns>
        public async Task<ResponseProducts> GetProductsByNameAsync(string name)
        {
            ResponseProducts response = new ResponseProducts { Status = StatusType.SUCCESS };
            if (response.Status == StatusType.ERROR)
            {
                return response;
            }
            response = await _etsyQuery.GetProductsByNameAsync(name);
            return response;
        }



        /// <summary>
        /// Recupera usuarios desde una API externa y almacena nuevos usuarios en la base de datos.
        /// Este método primero obtiene los usuarios de la API y verifica si ya existen en la base de datos
        /// basándose en sus direcciones de correo electrónico. Solo se añaden los nuevos usuarios que no existen en la base de datos.
        /// </summary>
        /// <returns>
        /// Un objeto ResponseUsers que contiene el estado de la operación, un mensaje que indica el resultado,
        /// y potencialmente la lista de usuarios recién añadidos. 
        /// </returns>
        /// <remarks>
        /// Este método realiza varias operaciones:
        /// 1. Llama a una API externa para obtener datos de los usuarios.
        /// 2. Recupera los correos electrónicos de usuarios existentes de la base de datos para verificar duplicados.
        /// 3. Filtra los usuarios existentes y prepara a los nuevos usuarios para su adición.
        /// 4. Guarda los nuevos usuarios en la base de datos si no existen previamente.
        /// 5. Devuelve una respuesta que indica el éxito o fracaso de estas operaciones.
        /// </remarks>
        /// 
        public async Task<ResponseUsers> GetUsersAsync()
        {
            ResponseUsers response = await _userQuery.GetUsersAsync();
            if(response.Status != StatusType.SUCCESS)
            {
                return response;
            }
             // Obtener los correos existentes desde la capa de datos.
            var existingEmails = new HashSet<string>(await _userQuery.GetExistingUserEmailsAsync(), StringComparer.OrdinalIgnoreCase);
            // Filtrar para obtener solo nuevos usuarios
            var newUsers = response.Data.Where(user => !existingEmails.Contains(user.Email)).Select(user => new User
            {
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                Name = new Name
                {
                    FirstName = user.Name.FirstName,
                    LastName = user.Name.LastName
                },
                Address = new Address
                {
                    City = user.Address.City,
                    Street = user.Address.Street,
                    Number = user.Address.Number,
                    Zipcode = user.Address.Zipcode,
                    Geolocation = new Geolocation
                    {
                        Latitude = user.Address.Geolocation.Latitude,
                        Longitude = user.Address.Geolocation.Longitude
                    }
                },
                Phone = user.Phone,
                Version = user.Version
            }).ToList();

            if (newUsers.Any())
            {
                await _userQuery.SaveUsersAsync(newUsers); // Guardar solo los nuevos usuarios.
                response.Message += $" Se han añadido {newUsers.Count} nuevos usuarios.";
            }
            else
            {
                response.Message += " No se encontraron nuevos usuarios para añadir.";
            }

            return response;
        }
    }
}
