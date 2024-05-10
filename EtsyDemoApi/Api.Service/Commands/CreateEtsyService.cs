
using Api.Data.Repository.Commands;
using Api.Data.Repository.Queries;
using Api.Data.Repository.Queries.Contracts;
using Api.Domain.Enum;
using Api.Domain.Request;
using Api.Domain.Response;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Service.Commands
{
    public class CreateEtsyService : ICreateEtsyService
    {
        private readonly HttpClient? _httpClient;
        private readonly string? _apiKey;
        private readonly IEtsyRepository _etsyRepository;
        private readonly IUserQuery _userQuery;


        public CreateEtsyService(HttpClient httpClient,
                          IConfiguration configuration,
                          ApiContext context,
                          IEtsyRepository etsyRepository,
                          IUserQuery userQuery)
        {
            _httpClient = httpClient;
            _apiKey = configuration["EtsyApiKey"];
            _etsyRepository = etsyRepository;
            _userQuery = userQuery;
        }

        public async Task<ResponseUser> CreateUserAsync(RegisterUserRequest registerUserRequest)
        {
            ResponseUser response = new ResponseUser { Status = StatusType.SUCCESS };
            if (response.Status == StatusType.ERROR)
            {
                return response;
            }
            response = await _etsyRepository.RegisterUserAsync(registerUserRequest);
            return response;

        }

        public async Task<ResponseUserToken> LogInUserAsync(LogInRequest logInRequest)
        {
            ResponseUserToken response = await _userQuery.LogInUserAsync(logInRequest);
            if (response.Status == StatusType.SUCCESS && response.Data != null)
            {
                string token = GenerateJwtToken(response.Data);
                response.Token = token;
                response.Message = "Inicio de sesión exitoso.";
            }


            return response;

        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")); 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Configura la duración del token según tus necesidades.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       
    }
}
