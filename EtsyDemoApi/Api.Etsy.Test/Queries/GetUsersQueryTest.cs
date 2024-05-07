using Api.Data.Repository.Queries;
using Api.Data.Repository.Queries.Contracts;
using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Models;
using Api.Service.Queries;
using Moq;

namespace Api.Test.Queries
{
    public class GetUsersQueryTest
    {
        private readonly IGetEtsyService _getEtsyService;
        private readonly Mock<IUserQuery> _mockUserQuery;
        public GetUsersQueryTest()
        {
            _mockUserQuery = new Mock<IUserQuery>();
            _getEtsyService = new GetEtsyService(null, _mockUserQuery.Object); // Asumiendo que tienes un constructor adecuado

            // Configuración inicial del mock para IUserQuery
            _mockUserQuery.Setup(x => x.GetUsersAsync())
                .ReturnsAsync(new ResponseUsers
                {
                    Status = StatusType.SUCCESS,
                    Data = new List<User>
                    {
                        new User { Email = "user1@example.com", Username = "user1" },
                        new User { Email = "user2@example.com", Username = "user2" }
                    },
                    Message = "Usuarios recuperados exitosamente"
                });

            _mockUserQuery.Setup(x => x.GetExistingUserEmailsAsync())
                .ReturnsAsync(new HashSet<string> { "user1@example.com" });
        }

        [Fact]
        public async Task GetUsersAsync_Returns_Only_New_Users()
        {
            // Arrange
            HashSet<string> existingEmails = new HashSet<string>(); // Assume no users exist for this test
            _mockUserQuery.Setup(x => x.GetUsersAsync())
                .ReturnsAsync(new ResponseUsers
                {
                    Status = StatusType.SUCCESS,
                    Data = new List<User>
                    {
                new User { Email = "user2@example.com", Username = "user2", Name = new Name(), Address = new Address{ Geolocation = new Geolocation() } }
                    },
                    Message = "Usuarios recuperados exitosamente"
                });
            _mockUserQuery.Setup(x => x.GetExistingUserEmailsAsync())
                .ReturnsAsync(existingEmails);

            // Act
            var result = await _getEtsyService.GetUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusType.SUCCESS, result.Status);
            Assert.Single(result.Data); // Verifies that exactly one new user was returned
            Assert.Contains(result.Data, u => u.Email == "user2@example.com"); // Verifies that the new user is the expected one
            Assert.True(result.Message.Contains("Se han añadido 1 nuevos usuarios"), "Expected message to confirm addition of new users");
        }

        [Fact]
        public async Task GetUsersAsync_No_New_Users_Found()
        {
            // Arrange
            var existingUsers = new List<User>
            {
                new User { Email = "user1@example.com", Username = "user1" }
            };

            _mockUserQuery.Setup(x => x.GetUsersAsync())
                        .ReturnsAsync(new ResponseUsers
                        {
                            Status = StatusType.SUCCESS,
                            Data = existingUsers,
                            Message = "Usuarios recuperados exitosamente"
                        });

            _mockUserQuery.Setup(x => x.GetExistingUserEmailsAsync())
                          .ReturnsAsync(new HashSet<string>(existingUsers.Select(u => u.Email)));

            // Act
            var result = await _getEtsyService.GetUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusType.SUCCESS, result.Status);
            Assert.Empty(result.Data); // Verifica que no se devolvieron nuevos usuarios
            Assert.True(result.Message.Contains("No se encontraron nuevos usuarios para añadir"));
        }
    

        [Fact]
        public async Task GetUsersAsync_Returns_Error_When_Failed()
        {
            // Arrange
            _mockUserQuery.Setup(x => x.GetUsersAsync())
                .ReturnsAsync(new ResponseUsers
                {
                    Status = StatusType.ERROR,
                    Message = "Error al recuperar usuarios"
                });

            // Act
            var result = await _getEtsyService.GetUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusType.ERROR, result.Status);
            Assert.Null(result.Data); // Verifica que no se devuelven datos cuando hay error
            Assert.Equal("Error al recuperar usuarios", result.Message);
        }
    }
}
