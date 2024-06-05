using Api.Data.Repository.Queries;
using Api.Data.Repository.Queries.Contracts;
using Api.Data.SeedWork;
using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;


namespace Api.Data.Repository.Commands
{
    public class EtsyRepository : EtsyRepositoryBase, IEtsyRepository
    {
        private readonly IUserQuery _userQuery;
        public EtsyRepository(ApiContext context, HttpClient httpClient, IUserQuery userQuery) : base(httpClient, context)
        {
            _userQuery = userQuery;
        }


        


        /// <summary>
        /// Registra nuevo ususario si no existe
        /// </summary>
        /// <param name="registerUserRequest"></param>
        /// <returns></returns>
        public async Task<ResponseUser> RegisterUserAsync(RegisterUserRequest registerUserRequest)
        {
            var existingUser = await _userQuery.GetUserByEmailOrUsernameAsync(registerUserRequest.Email, registerUserRequest.Username);
            try 
            {
                if (existingUser != null)
                {
                    _responseUser.Status = StatusType.ERROR;
                    _responseUser.Message = "El usuario ya existe";
                    return _responseUser;
                }
                else
                {
                    User user = new User {
                        Email = registerUserRequest.Email,
                        Username = registerUserRequest.Username,
                        Password = registerUserRequest.Password, // Remember to hash the password
                        Name = new Name
                        {
                            FirstName = registerUserRequest.Name.FirstName,
                            LastName = registerUserRequest.Name.LastName
                        },
                        Address = new Address
                        {
                            GeolocationId = registerUserRequest.Address.geolocation != null ? null : (int?)null,
                            Geolocation = registerUserRequest.Address.geolocation != null ? new Geolocation
                            {
                                Latitude = registerUserRequest.Address.geolocation.Latitude,
                                Longitude = registerUserRequest.Address.geolocation.Longitude
                            } : null,
                            City = registerUserRequest.Address.City,
                            Street = registerUserRequest.Address.Street,
                            Number = registerUserRequest.Address.Number,
                            Zipcode = registerUserRequest.Address.Zipcode
                        },
                        Phone = registerUserRequest.Phone
                    };
                    _context.Users.Add(user);
                    _responseUser.Message = "Usuario añadido correctamente";
                }
            } catch (Exception ex)
            {
                _responseUser.Message += ex.Message;
                _responseUser.Status = StatusType.ERROR;
            }
            await _context.SaveChangesAsync();
            return _responseUser;
        }
    }

}
