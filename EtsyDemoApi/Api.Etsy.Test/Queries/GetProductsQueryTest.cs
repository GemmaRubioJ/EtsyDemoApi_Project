using Api.Data.Repository.Queries;
using Api.Data.Repository.Queries.Contracts;
using Api.Domain.Enum;
using Api.Domain.Response;
using Api.Infraestructura.Models;
using Api.Service.Queries;
using Moq;


namespace Api.Test.Queries
{
    public class GetProductsQueryTest
    {
        private readonly IGetEtsyService _getEtsyService;
        private readonly EtsyQuery? _etsyQuery;
        private readonly UserQuery? _userQuery;
        private readonly Mock<IEtsyQuery> _mockEtsyQuery;
        private readonly Mock<IUserQuery> _mockUserQuery;

        public GetProductsQueryTest()
        {
            _mockEtsyQuery = new Mock<IEtsyQuery>();
            _mockUserQuery = new Mock<IUserQuery>();
            _getEtsyService = new GetEtsyService(_etsyQuery, _userQuery);
            _mockEtsyQuery.Setup(x => x.GetAllProductsAsync())
                      .ReturnsAsync(new ResponseProducts
                      {
                          Status = StatusType.SUCCESS,
                          Data = new List<Product>
                          {
                              new Product { ProductId = 1, Title = "Product 1" },
                              new Product { ProductId = 2, Title = "Product 2" }
                          },
                          Message = "Productos recolectados exitosamente"
                      });

            // Instanciar GetEtsyService utilizando el mock de IEtsyQuery
            _getEtsyService = new GetEtsyService(_mockEtsyQuery.Object, _mockUserQuery.Object);
        }


        /// <summary>
        /// Prueba que GetProductsAsync obtiene correctamente todos los productos cuando IEtsyQuery subyacente retorna una respuesta exitosa.
        /// Este test asegura que el servicio maneja correctamente y transmite los datos y el estado desde la capa de datos al controlador o a la siguiente capa superior.
        /// También verifica la integridad de los datos retornados, asegurándose de que la lista de productos y el mensaje de éxito sean como se espera.
        /// </summary>
        [Fact]
        public async Task Get_All_Products_Ok()
        {
            // Act
            var result = await _getEtsyService.GetProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusType.SUCCESS, result.Status);
            Assert.Equal(2, result.Data.Count()); // Asegúrate de que se devuelven los productos correctos
            Assert.Equal("Productos recolectados exitosamente", result.Message);
        }



        /// <summary>
        /// Prueba que GetProductsByNameAsync recupera exitosamente productos por nombre.
        /// Este método verifica que el servicio puede manejar respuestas exitosas de la capa de datos basadas en búsquedas de nombres de productos.
        /// Comprueba que el servicio retorna el estado correcto y filtra los productos según los criterios de búsqueda.
        /// </summary>
        [Fact]
        public async Task GetProductsByNameAsync_Returns_Successful_Response()
        {
            // Arrange
            string searchName = "testProduct";
            var expectedResponse = new ResponseProducts
            {
                Status = StatusType.SUCCESS,
                Data = new List<Product> { new Product { Title = searchName } }
            };
            _mockEtsyQuery.Setup(x => x.GetProductsByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _getEtsyService.GetProductsByNameAsync(searchName);

            // Assert
            Assert.Equal(StatusType.SUCCESS, result.Status);
            Assert.NotNull(result.Data);
        }


        /// <summary>
        /// Prueba la respuesta de GetProductsByNameAsync cuando no se encuentran productos que coincidan con los criterios de búsqueda.
        /// Este test verifica que el servicio maneja correctamente y comunica un estado de error cuando la capa de datos no encuentra productos coincidentes.
        /// Asegura que el error se maneje de manera adecuada y se retorne el mensaje de error apropiado.
        /// </summary>
        [Fact]
        public async Task GetProductsByNameAsync_Returns_Error_When_No_Products_Found()
        {
            // Arrange
            string searchName = "nonExistingProduct";
            var expectedResponse = new ResponseProducts
            {
                Status = StatusType.ERROR,
                Message = "No products found"
            };
            _mockEtsyQuery.Setup(x => x.GetProductsByNameAsync(searchName))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _getEtsyService.GetProductsByNameAsync(searchName);

            // Assert
            Assert.Equal(StatusType.ERROR, result.Status);
            Assert.Equal("No products found", result.Message);
        }
    }
}
