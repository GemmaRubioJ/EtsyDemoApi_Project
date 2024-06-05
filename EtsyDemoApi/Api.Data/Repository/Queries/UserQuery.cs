using Api.Data.Repository.Queries.Contracts;
using Api.Data.SeedWork;
using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;
using Azure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api.Data.Repository.Queries
{
    public class UserQuery : EtsyRepositoryBase, IUserQuery
    {
        public UserQuery(HttpClient httpclient, ApiContext context) : base(httpclient, context) { }



        /// <summary>
        /// Obtiene todos los usuarios de una Api Externa
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseUsers> GetUsersAsync()
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync("https://fakestoreapi.com/users");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
                    if (!users.Any())
                    {
                        _responseUsers.Status = StatusType.ERROR;
                        _responseUsers.Message = "Error al recolectar los usuarios";
                        return _responseUsers;
                    }
                    else
                    {
                        _responseUsers.Status = StatusType.SUCCESS;
                        _responseUsers.Message = "Usuarios recolectados exitosamente";
                        _responseUsers.Data = users;

                    }
                }
                else
                {
                    _responseUsers.Status = StatusType.ERROR;
                    _responseUsers.Message = "Error en la respuesta de la API";
                }
            }
            catch (Exception ex) 
            {
                _responseUsers.Message += ex.Message;
                _responseUsers.Status = StatusType.ERROR;
            }
            return _responseUsers;
        }


        /// <summary>
        /// Inserta una colección de usuarios en base de datos
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task SaveUsersAsync(IEnumerable<User> users)
        {
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Recupera todos los correos de los usuarios almacenados 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetExistingUserEmailsAsync()
        {
            // Utilizamos AsNoTracking porque no necesitamos realizar operaciones de actualización sobre estos objetos, 
            // lo que puede mejorar el rendimiento de la consulta.
            return await _context.Users.AsNoTracking()
                                       .Select(u => u.Email)
                                       .ToListAsync();
        }


        /// <summary>
        /// Busca un usuario por su correo y su nombre de usuario
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmailOrUsernameAsync(string email, string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email || u.Username == username);
        }


        /// <summary>
        /// Busca un usuario por su Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


        /// <summary>
        /// Busca un usuario por su Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }


        /// <summary>
        /// Verifica las credenciales de un usuario para iniciar sesión
        /// </summary>
        /// <param name="logInRequest"></param>
        /// <returns></returns>
        public async Task<ResponseUserToken> LogInUserAsync(LogInRequest logInRequest)
        {
            try
            {
                var user = await GetUserByEmail(logInRequest.Email);
                if (user == null)
                {
                    _responseUserToken.Status = StatusType.ERROR;
                    _responseUserToken.Message = "Usuario no encontrado.";
                    return _responseUserToken;
                }
                if (user.Password != logInRequest.Password) // Cambiar esto por la comprobación del hash más adelante
                {
                    _responseUserToken.Status = StatusType.ERROR;
                    _responseUserToken.Message = "Contraseña incorrecta.";
                    return _responseUserToken;
                }
                _responseUserToken.Data = user;
                _responseUserToken.Message = "Incio de sesión exitoso";
            }
            catch (Exception ex)
            {
                _responseUserToken.Message += ex.Message;
                _responseUserToken.Status = StatusType.ERROR;
            }
            return _responseUserToken;
        }

    }
}
